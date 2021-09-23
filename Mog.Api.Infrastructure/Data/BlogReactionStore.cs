using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Abstractions;

namespace Mog.Api.Infrastructure.Data
{
    public class BlogReactionStore : IStore<object[]>
    {
        private AsheDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BlogReactionStore(
            AsheDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<object[]> AddAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }

        public async Task<object[]> UpdateAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == new Guid(reaction[0].ToString()));

            if (reaction[1].ToString() == "like")
            {
                blog.Like++;
            }

            if (reaction[1].ToString() == "dislike")
            {
                blog.Dislike++;
            }

            if (reaction[1].ToString() == "love")
            {
                blog.Love++;
            }

            _context.SaveChanges();
            return reaction;
        }

        public async Task<object[]> DeleteAsync(object[] reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }
    }
}