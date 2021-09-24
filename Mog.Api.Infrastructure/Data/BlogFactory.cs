using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class BlogFactory : IFactory<IQueryable<Blog>, Guid>
    {
        private AsheDbContext _context;

        public BlogFactory(
            AsheDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Blog>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Blog> blogs = _context.Blogs.OrderByDescending(x => x.Created);
            return blogs;
        }

        public async Task<IQueryable<Blog>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Blog> blogs = _context.Blogs
                                        .Where(x => x.Id == id);
            return blogs;
        }
    }
}