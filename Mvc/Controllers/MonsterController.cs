using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mvc.Models;

namespace Mvc.Controllers
{
    
    [Authorize]
    public class MonsterController : Controller
    {
        private readonly CharacterContext _context;

        public MonsterController(CharacterContext context)
        {
            _context = context;
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
        public async Task<IActionResult> Details(int? id)
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
        public async Task<IActionResult> Create([Bind("MonsterId,Name,Strength,Weakness,Description,Picture,Games")] Monster monster)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monster);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(monster);
        }

        // GET: Monster/Edit/5
        public async Task<IActionResult> Edit(int? id)
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
        public async Task<IActionResult> Edit(int id, [Bind("MonsterId,Name,Strength,Weakness,Description,Picture,Games")] Monster monsters)
        {
            if (id != monsters.MonsterId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monsters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonsterExists(monsters.MonsterId))
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
            return View(monsters);
        }

        // GET: Monster/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Monster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var monsters = await _context.Monsters.SingleOrDefaultAsync(m => m.MonsterId == id);
            _context.Monsters.Remove(monsters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonsterExists(int id)
        {
            return _context.Monsters.Any(e => e.MonsterId == id);
        }
    }
}
