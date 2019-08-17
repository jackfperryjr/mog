using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moogle.Data;
using Moogle.Models;
using Moogle.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Moogle.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public BlogController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        // GET: Articles
        public async Task<IActionResult> Index()
        {
            var posts = _context.BlogPosts.Include(a => a.Author);
            return View(await posts.ToListAsync());
        }

        // GET: Articles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var posts = await _context.BlogPosts
                                        .Include(a => a.Author)
                                        .SingleOrDefaultAsync(b => b.BlogPostId == id);
            if (posts == null)
            {
                return NotFound();
            }

            return View(posts);
        }

        // GET: Articles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Content")] BlogPost post)
        {
            if (ModelState.IsValid)
            {
                post.AuthorId = _userManager.GetUserId(this.User);
                post.Author = await _userManager.GetUserAsync(this.User);
                post.CreatedDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                if (post.AuthorId != "5f6f5e73-47b8-4610-ae30-9c062716c86d")
                {
                    await _emailSender.SendBlogPostToAdminEmailAsync(post.Content, post.Author.FirstName, post.Author.Email);
                }

                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Articles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.BlogPosts.SingleOrDefaultAsync(b => b.BlogPostId == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.BlogPosts.SingleOrDefaultAsync(b => b.BlogPostId == id);
            _context.BlogPosts.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}