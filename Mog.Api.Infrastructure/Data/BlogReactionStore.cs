using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Abstractions;

namespace Mog.Api.Infrastructure.Data
{
    public class BlogReactionStore : IStore<KeyValuePair<Guid, string>>
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

        public async Task<KeyValuePair<Guid, string>> AddAsync(KeyValuePair<Guid, string> reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }

        public async Task<KeyValuePair<Guid, string>> UpdateAsync(KeyValuePair<Guid, string> reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            var blog = await _context.Blogs.FirstOrDefaultAsync(x => x.Id == reaction.Key);

            if (reaction.Value == "like")
            {
                blog.Like++;
            }

            if (reaction.Value == "dislike")
            {
                blog.Dislike++;
            }

            if (reaction.Value == "love")
            {
                blog.Love++;
            }

            _context.SaveChanges();
            return reaction;
        }

        public async Task<KeyValuePair<Guid, string>> DeleteAsync(KeyValuePair<Guid, string> reaction, CancellationToken cancellationToken = new CancellationToken())
        {
            return reaction;
        }
    }
}