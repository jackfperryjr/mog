using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Abstractions;

namespace Mog.Api.Infrastructure.Data
{
    public class FeedStore : IStore<object[]>
    {
        private SerahDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FeedStore(
            SerahDbContext context,
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
            var feed = await _context.Feed.FirstOrDefaultAsync(x => x.Id == new Guid(reaction[0].ToString()));

            if (reaction[1].ToString() == "like")
            {
                feed.Like++;
            }

            if (reaction[1].ToString() == "dislike")
            {
                feed.Dislike++;
            }

            if (reaction[1].ToString() == "love")
            {
                feed.Love++;
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