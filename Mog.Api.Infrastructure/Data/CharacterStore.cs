using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Extensions;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class CharacterStore : IStore<Character>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterStore(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Character> AddAsync(Character model, CancellationToken cancellationToken = new CancellationToken())
        {
            throw new NotImplementedException();
        }

        public async Task<Character> UpdateAsync(Character model, CancellationToken cancellationToken = new CancellationToken())
        {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);

            // TODO: Update records.

            return model;
        }

        public async Task<Character> DeleteAsync(Character model, CancellationToken cancellationToken = new CancellationToken())
        {
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);
            var stats = await _context.Stats.Where(x => x.CollectionId == model.Id).ToListAsync();
            var pictures = await _context.Pictures.Where(x => x.CollectionId == model.Id).ToListAsync();
            var profile = await _context.DatingProfile.FirstOrDefaultAsync(x => x.CharacterId == model.Id);
            IList<DatingResponse> responses;
            
            if (profile != null) 
            {
                responses = await _context.Responses.Where(x => x.DatingProfileId == profile.Id).ToListAsync();
                _context.Responses.RemoveRange(responses);
                _context.DatingProfile.Remove(profile);
            }

            _context.Stats.RemoveRange(stats);
            _context.Pictures.RemoveRange(pictures);
            _context.Characters.Remove(character);
            _context.SaveChanges();

            return model;
        }
    }
}