using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Moogle.Data;
using Moogle.Models;

namespace Moogle.Controllers
{

    [Authorize]
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env;

        public GameController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

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
            if (ModelState.IsValid)
            {
                _context.Add(game);
            }
            await _context.SaveChangesAsync();

            string webRootPath = _env.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var gameFromDb = _context.Games.Find(game.GameId);

            if (files.Count != 0) 
            {
                var upload = Path.Combine(webRootPath, @"images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(upload, "Game-" + game.Title + "-Picture" + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                gameFromDb.Picture = @"\" + @"images" + @"\" + "Game-" + game.Title + "-Picture" + extension;
            }
            else 
            {
                //var upload = Path.Combine(webRootPath, @"images", "default-image.png");
                //System.IO.File.Copy(upload, webRootPath + @"\" + @"images" + @"\" + game.GameId + ".png");
                gameFromDb.Picture = @"\" + @"icons" + @"\" + "icon-default-image.png";
            }
            //return View(game);
            await _context.SaveChangesAsync();
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
                    gameFromDb.Platform = game.Platform;
                    gameFromDb.ReleaseDate = game.ReleaseDate;
                    gameFromDb.Description = game.Description;

                    string webRootPath = _env.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    if (game.Picture != gameFromDb.Picture) 
                    {
                        if (files.Count != 0 ) 
                        {
                            var upload = Path.Combine(webRootPath, @"images");
                            var extension = Path.GetExtension(files[0].FileName);

                            using (var filestream = new FileStream(Path.Combine(upload, "game-" + game.GameId + "-Picture" + extension), FileMode.Create))
                            {
                                files[0].CopyTo(filestream);
                            }
                            gameFromDb.Picture = @"\" + @"images" + @"\" + "game-" + game.GameId + "-Picture" + extension;
                        }
                        else
                        {
                            game.Picture = gameFromDb.Picture;
                        }
                    }
                    // else 
                    // {
                    //     gameFromDb.Picture = game.Picture;
                    // }

                    //_context.Update(gameFromDb);
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
                return RedirectToAction(nameof(Index));
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
