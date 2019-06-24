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
using Moogle.Data;
using Moogle.Models;

namespace Moogle.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env { get; }

        public CharacterController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: Character
        public async Task<IActionResult> Index(string currentFilter, string sortOrder, string searchString, int? page)
        {
            ViewData["NameSort"] = String.IsNullOrEmpty(sortOrder) ? "Name" : "";
            ViewData["OriginSort"] = String.IsNullOrEmpty(sortOrder) ? "Origin" : "";
            ViewData["CurrentFilter"] = searchString;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var characters = from c in _context.Character select c;
            characters = characters.OrderByDescending(c => c.Origin ).ThenBy(c => c.Name);

            if (!String.IsNullOrEmpty(searchString))
            {
                characters = characters.Where(c => c.Name.Contains(searchString.First().ToString().ToUpper() + searchString.Substring(1)) || c.Origin.Contains(searchString));
            }

            int pageSize = 16;
            return View(await PaginatedList<Characters>.CreateAsync(characters.AsNoTracking(), page ?? 1, pageSize));
            //return View(await characters.AsNoTracking()/*.Take(10)*/.ToListAsync());
        }

        // GET: Character/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Character
                .SingleOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // GET: Character/Create

        [Authorize(Roles="Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Character/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Age,Gender,Race,Job,Height,Weight,Origin,Description,Picture,Picture2,Picture3,Picture4,Picture5,Response1,Response2,Response3,Response4,Response5,Response6,Response7,Response8,Response9,Response10")] Characters characters)
        {
            if (ModelState.IsValid)
            {
                _context.Add(characters);
                // await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
            }
            await _context.SaveChangesAsync();

            string webRootPath = _env.WebRootPath;
            var files = HttpContext.Request.Form.Files;
            var characterFromDb = _context.Character.Find(characters.Id);

            if (files.Count != 0) 
            {
                for (var i = 0; i < files.Count; i++) {
                    var upload = Path.Combine(webRootPath, @"images");
                    var extension = Path.GetExtension(files[i].FileName);

                    using (var filestream = new FileStream(Path.Combine(upload, "Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension), FileMode.Create))
                    {
                        files[i].CopyTo(filestream);
                        filestream.Flush();
                    }
                    if (i == 0) 
                    {
                        characterFromDb.Picture = @"\" + @"images" + @"\" + "Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 1) 
                    {
                        characterFromDb.Picture2 = @"\" + @"images" + @"\" + "Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 2) 
                    {
                        characterFromDb.Picture3 = @"\" + @"images" + @"\" + "Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 3) 
                    {
                        characterFromDb.Picture4 = @"\" + @"images" + @"\" + "Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 4) 
                    {
                        characterFromDb.Picture5 = @"\" + @"images" + @"\" + "Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                }
            }

            // else 
            // {
            //     var upload = Path.Combine(webRootPath, @"images", "default-image.png");
            //     System.IO.File.Copy(upload, webRootPath + @"\" + @"images" + @"\" + characters.Id + ".png");
            //     characterFromDb.Picture = @"\" + @"images" + @"\" + "default-image.png";
            // }


            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //return View(characters);
        }

        // GET: Character/Edit/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Character.SingleOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }
            return View(characters);
        }

        // POST: Character/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Age,Gender,Race,Job,Height,Weight,Origin,Description,Picture,Picture2,Picture3,Picture4,Picture5,Response1,Response2,Response3,Response4,Response5,Response6,Response7,Response8,Response9,Response10")] Characters characters)
        {
            if (id != characters.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(characters);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CharactersExists(characters.Id))
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
            return View(characters);
        }

        // GET: Character/Delete/5
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var characters = await _context.Character
                .SingleOrDefaultAsync(m => m.Id == id);
            if (characters == null)
            {
                return NotFound();
            }

            return View(characters);
        }

        // POST: Character/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var characters = await _context.Character.SingleOrDefaultAsync(m => m.Id == id);
            _context.Character.Remove(characters);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CharactersExists(Guid id)
        {
            return _context.Character.Any(e => e.Id == id);
        }
    }
}
