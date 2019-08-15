using Moogle.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Moogle.ViewComponents
{
    public class LatestBlogsViewComponent: ViewComponent
    {
        private readonly ApplicationDbContext _context;

        public LatestBlogsViewComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync(int howMany = 3)
        {
            var lastBlogs = await _context.BlogPosts
                                            .OrderByDescending(a => a.CreatedDate)
                                            .Take(howMany)
                                            .ToListAsync();
            return View(lastBlogs);
        }
    }
}