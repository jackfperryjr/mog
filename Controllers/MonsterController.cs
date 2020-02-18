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
using Mog.Services;
using Mog.Models;
using Mog.Data;

namespace Mog.Controllers
{
    
    [Authorize(Roles="Admin, Manager")]
    public class MonsterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration;

        public MonsterController(
            ApplicationDbContext context, 
            IEmailSender emailSender, 
            UserManager<ApplicationUser> userManager, 
            IConfiguration configuration)   
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _configuration = configuration;
        }

        public static IConfiguration configuration { get; private set; }

        // GET: Monster
        public async Task<IActionResult> Index(string currentFilter, string sortOrder, string searchString, int? page)
        {
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var monsters = from m in _context.Monsters select m;
            monsters = monsters.OrderBy(m => m.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                monsters = monsters.Where(m => m.Name.Contains(searchString.First().ToString().ToUpper() + searchString.Substring(1))); // Search by name of monster
            }

            int pageSize = 14;
            return View(await PaginatedList<Monster>.CreateAsync(monsters.AsNoTracking(), page ?? 1, pageSize));
        }

        // GET: Monster/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monsters = await _context.Monsters
                .SingleOrDefaultAsync(m => m.MonsterId == id);
            if (monsters == null)
            {
                return NotFound();
            }

            return View(monsters);
        }

        // GET: Monster/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MonsterId,Name,JapaneseName,ElementalAffinity,ElementalWeakness,HitPoints,ManaPoints,Attack,Defense,Description,Picture,Game,AddedBy")] Monster monster)
        {
            var account = _configuration["AzureStorageConfig:AccountName"];
            var key = _configuration["AzureStorageConfig:AccountKey"];
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();

            // if (ModelState.IsValid) // Until I figure out why it's false.
            // {
                _context.Add(monster);
            //}
            await _context.SaveChangesAsync();

            var files = HttpContext.Request.Form.Files;
            var monsterFromDb = _context.Monsters.Find(monster.MonsterId);

            if (files.Count != 0) 
            {
                var extension = Path.GetExtension(files[0].FileName);
                var newBlob = container.GetBlockBlobReference("Monster-" + monster.MonsterId + extension);

                using (var filestream = new MemoryStream())
                {
                    files[0].CopyTo(filestream);
                    filestream.Position = 0;
                    await newBlob.UploadFromStreamAsync(filestream);
                }
                monsterFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/Monster-" + monster.MonsterId + extension;
            }
            else 
            {
                monsterFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/icon-default-image.png";
            }
            //return View(monster);

            if (!User.IsInRole("Admin")) {
                var user = await _userManager.GetUserAsync(User);
                await _emailSender.SendUpdateEmailAsync("Monster added", user.FirstName, user.Email, "to", "added");
            }

            await _context.SaveChangesAsync();
            TempData["ClassName"] = "bg-success";
            TempData["ContainerHeight"] = "height: 50px; border-radius: 5px;";
            TempData["Message"] = "Monster added!";
            TempData["Status"] = "Success";
            return RedirectToAction(nameof(Index));
        }

        // GET: Monster/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monsters = await _context.Monsters.SingleOrDefaultAsync(m => m.MonsterId == id);
            if (monsters == null)
            {
                return NotFound();
            }
            return View(monsters);
        }

        // POST: Monster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("MonsterId,Name,JapaneseName,ElementalAffinity,ElementalWeakness,HitPoints,ManaPoints,Attack,Defense,Description,Picture,Game,AddedBy")] Monster monster)
        {
            var account = _configuration["AzureStorageConfig:AccountName"];
            var key = _configuration["AzureStorageConfig:AccountKey"];
            var storageCredentials = new StorageCredentials(account, key);
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();

            if (id != monster.MonsterId)
            {
                return NotFound();
            }

            var monsterFromDb = await _context.Monsters.SingleOrDefaultAsync(m => m.MonsterId == id);

            if (ModelState.IsValid)
            {
                try
                {
                    monsterFromDb.Name = monster.Name;
                    monsterFromDb.JapaneseName = monster.JapaneseName;
                    monsterFromDb.ElementalAffinity = monster.ElementalAffinity;
                    monsterFromDb.ElementalWeakness = monster.ElementalWeakness;
                    monsterFromDb.HitPoints = monster.HitPoints;
                    monsterFromDb.ManaPoints = monster.ManaPoints;
                    monsterFromDb.Attack = monster.Attack;
                    monsterFromDb.Defense = monster.Defense;
                    monsterFromDb.Description = monster.Description;
                    monsterFromDb.Game = monster.Game;
                    monsterFromDb.AddedBy = monster.AddedBy;

                    var files = HttpContext.Request.Form.Files;

                    if (monster.Picture != monsterFromDb.Picture) 
                    {
                        if (files.Count != 0 ) 
                        {
                            var extension = Path.GetExtension(files[0].FileName);
                            var newBlob = container.GetBlockBlobReference("Monster-" + monster.MonsterId + extension);

                            using (var filestream = new MemoryStream())
                            {
                                files[0].CopyTo(filestream);
                                filestream.Position = 0;
                                await newBlob.UploadFromStreamAsync(filestream);
                            }
                                monsterFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/Monster-" + monster.MonsterId + extension;
                        }
                    }
                    TempData["ClassName"] = "bg-success";
                    TempData["ContainerHeight"] = "height: 50px; border-radius: 5px;";
                    TempData["Message"] = "Monster updated!";
                    TempData["Status"] = "Success";
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonsterExists(monsterFromDb.MonsterId))
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
            return View(monsterFromDb);
        }

        // GET: Monster/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var monster = await _context.Monsters
                .SingleOrDefaultAsync(m => m.MonsterId == id);
            if (monster == null)
            {
                return NotFound();
            }

            return View(monster);
        }

        // POST: Monster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var monster = await _context.Monsters.SingleOrDefaultAsync(m => m.MonsterId == id);
            _context.Monsters.Remove(monster);

            var user = await _userManager.GetUserAsync(User);
            await _emailSender.SendUpdateEmailAsync("Monster deleted", user.FirstName, user.Email, "from", "deleted");

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonsterExists(Guid id)
        {
            return _context.Monsters.Any(e => e.MonsterId == id);
        }
    }
}
