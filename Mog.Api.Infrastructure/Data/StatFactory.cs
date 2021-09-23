using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class StatFactory : IFactory<IQueryable<Stat>, Guid>
    {
        private SerahDbContext _context;

        public StatFactory(
            SerahDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Stat>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Stat> stats = _context.Stats;
            stats = stats.OrderBy(x => x.Platform);
            return stats;
        }

        public async Task<IQueryable<Stat>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Stat> stats = _context.Stats.Where(x => x.Id == id);
            return stats;
        }
    }
}