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
    public class CharacterFactory : IFactory<IQueryable<Character>, Guid>
    {
        private ApplicationDbContext _context;

        public CharacterFactory(
            ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Character>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<Character> characters = _context.Characters
                                                .Include(x => x.Pictures)
                                                .Include(x => x.Stats);
            characters = characters.OrderBy(x => x.Origin).ThenBy(x => x.Name);
            return characters;
        }

        public async Task<IQueryable<Character>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {

            IQueryable<Character> characters = _context.Characters
                                        .Include(x => x.Pictures)
                                        .Include(x => x.Stats)
                                        .Where(x => x.Id == id);
            return characters;
        }
    }
}