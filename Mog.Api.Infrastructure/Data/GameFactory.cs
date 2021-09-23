using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class GameFactory : IFactory<IQueryable<Game>, Guid>
    {
        private SerahDbContext _context;

        public GameFactory(
            SerahDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Game>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Game> games = _context.Games;
            games = games.OrderBy(x => x.Title);
            return games;
        }

        public async Task<IQueryable<Game>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {

            IQueryable<Game> games = _context.Games.Where(x => x.GameId == id);
            return games;
        }
    }
}