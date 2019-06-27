using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moogle.Services;
using Moogle.Models;
using Moogle.Data;

namespace Moogle.Controllers
{
    
    [Authorize]
    public class MonsterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private IHostingEnvironment _env { get; }

        public MonsterController(
            ApplicationDbContext context, 
            IEmailSender emailSender, 
            UserManager<ApplicationUser> userManager, 
            IHostingEnvironment env)
        {
            _context = context;
            _emailSender = emailSender;
            _userManager = userManager;
            _env = env;
        }

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

            int pageSize = 10;
            return View(await PaginatedList<Monster>.CreateAsync(monsters.AsNoTracking(), page ?? 1, pageSize));
            //return View(await characters.AsNoTracking()/*.Take(10)*/.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("MonsterId,Name,Strength,Weakness,Description,Picture")] Monster monster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monster);
            }
            await _context.SaveChangesAsync();

            string webRootPath = _env.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var monsterFromDb = _context.Monsters.Find(monster.MonsterId);

            if (files.Count != 0) 
            {
                var upload = Path.Combine(webRootPath, @"images");
                var extension = Path.GetExtension(files[0].FileName);

                using (var filestream = new FileStream(Path.Combine(upload, "Monster-" + monster.MonsterId + "-Picture" + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);
                }
                monsterFromDb.Picture = @"\" + @"images" + @"\" + "Monster-" + monster.MonsterId + "-Picture" + extension;
            }
            else 
            {
                //var upload = Path.Combine(webRootPath, @"images", "default-image.png");
                //System.IO.File.Copy(upload, webRootPath + @"\" + @"images" + @"\" + monster.MonsterId + ".png");
                monsterFromDb.Picture = @"\" + @"icons" + @"\" + "icon-default-image.png";
            }
            //return View(monster);

            var user = await _userManager.GetUserAsync(User);
            await _emailSender.SendUpdateEmailAsync("Monster added", user.FirstName, user.Email, "to", "added");

            await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Edit(Guid id, [Bind("MonsterId,Name,Strength,Weakness,Description,Picture")] Monster monster)
        {
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
                    monsterFromDb.Strength = monster.Strength;
                    monsterFromDb.Weakness = monster.Weakness;
                    monsterFromDb.Description = monster.Description;

                    string webRootPath = _env.WebRootPath;
                    var files = HttpContext.Request.Form.Files;

                    if (monster.Picture != monsterFromDb.Picture) 
                    {
                        if (files.Count != 0 ) 
                        {
                            var upload = Path.Combine(webRootPath, @"images");
                            var extension = Path.GetExtension(files[0].FileName);

                            using (var filestream = new FileStream(Path.Combine(upload, "Monster-" + monster.MonsterId + "-Picture" + extension), FileMode.Create))
                            {
                                files[0].CopyTo(filestream);
                            }
                            monsterFromDb.Picture = @"\" + @"images" + @"\" + "Monster-" + monster.MonsterId + "-Picture" + extension;
                        }
                        else
                        {
                            monster.Picture = monsterFromDb.Picture;
                        }
                    }
                    // else 
                    // {
                    //     monsterFromDb.Picture = monster.Picture;
                    // }

                    //_context.Update(monsterFromDb);
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
