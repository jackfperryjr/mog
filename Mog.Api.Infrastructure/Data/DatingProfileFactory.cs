using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class DatingProfileFactory : IFactory<IQueryable<DatingProfile>, Guid>
    {
        private SerahDbContext _context;

        public DatingProfileFactory(
            SerahDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<DatingProfile>> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<DatingProfile> datingProfiles = _context.DatingProfile
                                                                .Include(x => x.Responses)
                                                                .Include(x => x.Character)
                                                                .ThenInclude(x => x.Pictures);
            datingProfiles = datingProfiles.OrderBy(x => x.Character.Name);
            return datingProfiles;
        }

        public async Task<IQueryable<DatingProfile>> GetByKeyAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            IQueryable<DatingProfile> datingProfiles = _context.DatingProfile
                                                                .Include(x => x.Responses)
                                                                .Include(x => x.Character)
                                                                .ThenInclude(x => x.Pictures)
                                                                .Where(x => x.CharacterId == id);
            return datingProfiles;
        }
    }
}