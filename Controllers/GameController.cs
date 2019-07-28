using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.Extensions.Configuration;
using Moogle.Services;
using Moogle.Models;
using Moogle.Data;

namespace Moogle.Controllers
{

    [Authorize]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _env { get; }
        private IConfiguration _configuration;

        public GameController(
            ApplicationDbContext context, 
            IEmailSender emailSender, 
            UserManager<ApplicationUser> userManager, 
            IHostingEnvironment env,
            IConfiguration configuration)   
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _env = env;
            _configuration = configuration;
        }

        public static IConfiguration configuration { get; private set; }

        // GET: Game
        public async Task<IActionResult> Index(string currentFilter, string sortOrder, string searchString, int? page)
        {
            ViewData["TitleSort"] = String.IsNullOrEmpty(sortOrder) ? "Title" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var games = from g in _context.Games select g;
            games = games.OrderBy(g => g.Title);

            if (!String.IsNullOrEmpty(searchString))
            {
                games = games.Where(g => g.Title.Contains(searchString.First().ToString().ToUpper() + searchString.Substring(1))); // Search by game title
            }

            //int pageSize = 10;
            //return View(await PaginatedList<Game>.CreateAsync(games.AsNoTracking(), page ?? 1, pageSize));
            return View(await games.AsNoTracking().ToListAsync());
        }

        // GET: Game/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .SingleOrDefaultAsync(g => g.GameId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // GET: Game/Create
        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([Bind("GameId,Title,Picture,Platform,ReleaseDate,Description")] Game game)
        {
            var account = _configuration["AzureStorageConfig:AccountName"];
            var key = _configuration["AzureStorageConfig:AccountKey"];
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();

            if (ModelState.IsValid)
            {
                _context.Add(game);
            }
            await _context.SaveChangesAsync();

            var files = HttpContext.Request.Form.Files;
            var gameFromDb = _context.Games.Find(game.GameId);

            if (files.Count != 0) 
            {
                var extension = Path.GetExtension(files[0].FileName);
                var newBlob = container.GetBlockBlobReference("Game-" + game.GameId + extension);

                using (var filestream = new MemoryStream())
                {
                    files[0].CopyTo(filestream);
                    filestream.Position = 0;
                    await newBlob.UploadFromStreamAsync(filestream);
                }
                gameFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/Game-" + game.GameId + extension;
            }
            else 
            {
                gameFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/icon-default-image.png";
            }
            //return View(monster);

            if (!User.IsInRole("Admin")) {
                var user = await _userManager.GetUserAsync(User);
                await _emailSender.SendUpdateEmailAsync("Monster added", user.FirstName, user.Email, "to", "added");
            }

            await _context.SaveChangesAsync();
            TempData["ClassName"] = "bg-success";
            TempData["ContainerHeight"] = "height: 50px; border-radius: 5px;";
            TempData["Message"] = "Game added!";
            TempData["Status"] = "Success";
            return RedirectToAction(nameof(Index));
        }

        // GET: Game/Edit/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games.SingleOrDefaultAsync(g => g.GameId == id);
            if (games == null)
            {
                return NotFound();
            }
            return View(games);
        }

        // POST: Game/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameId,Title,ReleaseDate,Platform,Picture,Description")] Game game)
        {
            var account = _configuration["AzureStorageConfig:AccountName"];
            var key = _configuration["AzureStorageConfig:AccountKey"];
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();

            if (id != game.GameId)
            {
                return NotFound();
            }

            var gameFromDb = await _context.Games.SingleOrDefaultAsync(g => g.GameId == id);

            if (ModelState.IsValid)
            {
                try
                {
                    gameFromDb.Title = game.Title;
                    gameFromDb.ReleaseDate = game.ReleaseDate;
                    gameFromDb.Platform = game.Platform;
                    gameFromDb.Description = game.Description;

                    var files = HttpContext.Request.Form.Files;

                    if (game.Picture != gameFromDb.Picture) 
                    {
                        if (files.Count != 0 ) 
                        {
                            var extension = Path.GetExtension(files[0].FileName);
                            var newBlob = container.GetBlockBlobReference("Game-" + game.GameId + extension);

                            using (var filestream = new MemoryStream())
                            {
                                files[0].CopyTo(filestream);
                                filestream.Position = 0;
                                await newBlob.UploadFromStreamAsync(filestream);
                            }
                                gameFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/Game-" + game.GameId + extension;
                        }
                    }
                    TempData["ClassName"] = "bg-success";
                    TempData["ContainerHeight"] = "height: 50px; border-radius: 5px;";
                    TempData["Message"] = "Game updated!";
                    TempData["Status"] = "Success";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(gameFromDb.GameId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Edit));
            }
            return View(gameFromDb);
        }

        // GET: Game/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var games = await _context.Games
                .SingleOrDefaultAsync(g => g.GameId == id);
            if (games == null)
            {
                return NotFound();
            }

            return View(games);
        }

        // POST: Game/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var game = await _context.Games.SingleOrDefaultAsync(g => g.GameId == id);
            _context.Games.Remove(game);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(Guid id)
        {
            return _context.Games.Any(e => e.GameId == id);
        }
    }
}
