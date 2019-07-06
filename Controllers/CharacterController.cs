using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.Extensions.Options;
using Moogle.Services;
using Moogle.Data;
using Moogle.Models;

namespace Moogle.Controllers
{
    [Authorize]
    public class CharacterController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _env { get; }

        public CharacterController(
            ApplicationDbContext context, 
            IHostingEnvironment env,
            IOptions<BlobStorageOptions> blob)
        {
            _context = context;
            _env = env;
            _credentials = blob.Value;
        }

        public BlobStorageOptions _credentials { get; } //set only via Secret Manager

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

            int pageSize = 14;
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
            var account = _credentials.BlobAccount;
            var key = _credentials.BlobKey;;
            var storageCredentials = new StorageCredentials("mooglestorage", "Jg1KxwT7fkXhfU4qMcvntBexSNUelBF2IhXF0gifBdOBMZRhmA9resnjm5FL5Ty1wfO4fyKt29vCjEFfhuSuKw==");
            var cloudStorageAccount = new CloudStorageAccount(storageCredentials, true);
            var cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
            var container = cloudBlobClient.GetContainerReference("images");
            await container.CreateIfNotExistsAsync();

            if (ModelState.IsValid)
            {
                _context.Add(characters);
                // await _context.SaveChangesAsync();
                // return RedirectToAction(nameof(Index));
            }
            await _context.SaveChangesAsync();

            var files = HttpContext.Request.Form.Files;
            var characterFromDb = _context.Character.Find(characters.Id);

            if (files.Count != 0) 
            {
                for (var i = 0; i < files.Count; i++) {
                    var extension = Path.GetExtension(files[i].FileName);
                    var newBlob = container.GetBlockBlobReference("Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension);

                    using (var filestream = new MemoryStream())
                    {
                        files[i].CopyTo(filestream);
                        filestream.Position = 0;
                        await newBlob.UploadFromStreamAsync(filestream);
                    }
                    if (i == 0) 
                    {
                        characterFromDb.Picture = "https://mooglestorage.blob.core.windows.net/images/Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 1) 
                    {
                        characterFromDb.Picture2 = "https://mooglestorage.blob.core.windows.net/images/Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 2) 
                    {
                        characterFromDb.Picture3 = "https://mooglestorage.blob.core.windows.net/images/Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 3) 
                    {
                        characterFromDb.Picture4 = "https://mooglestorage.blob.core.windows.net/images/Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                    if (i == 4) 
                    {
                        characterFromDb.Picture5 = "https://mooglestorage.blob.core.windows.net/images/Character-" + characters.Id + "-Picture" + (i + 1).ToString() + extension;
                    }
                }
            }

            // else 
            // {
            //     characterFromDb.Picture = @"\" + @"icons" + @"\" + "icon-default-image.png";
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
