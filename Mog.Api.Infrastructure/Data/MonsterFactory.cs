using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class MonsterFactory : IFactory<IQueryable<Monster>, Guid>
    {
        private SerahDbContext _context;

        public MonsterFactory(
            SerahDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Monster>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Monster> monsters = _context.Monsters;
            monsters = monsters.OrderBy(x => x.Name);
            return monsters;
        }

        public async Task<IQueryable<Monster>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {

            IQueryable<Monster> monsters = _context.Monsters.Where(x => x.MonsterId == id);
            return monsters;
        }
    }
}