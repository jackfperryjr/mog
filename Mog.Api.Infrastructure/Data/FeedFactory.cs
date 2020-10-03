using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class FeedFactory : IFactory<IQueryable<Feed>, Guid>
    {
        private ApplicationDbContext _context;

        public FeedFactory(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Feed>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Feed> feed = _context.Feed;
            feed = feed.OrderByDescending(x => x.TimeStamp);
            foreach (var x in feed)
            {
                DateTime convertedDate = DateTime.SpecifyKind(x.TimeStamp, DateTimeKind.Utc);
                x.TimeStamp = convertedDate.ToLocalTime();
            }
            
            return feed;
        }

        public async Task<IQueryable<Feed>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }
    }
}