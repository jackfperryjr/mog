using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Mog.Api.Core.Extensions;
using Mog.Api.Core.Abstractions;
using Mog.Api.Core.Models;

namespace Mog.Api.Infrastructure.Data
{
    public class CharacterStore : IStore<Character>
    {
        private SerahDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterStore(
            SerahDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Character> AddAsync(Character model, CancellationToken cancellationToken = new CancellationToken())
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("sub")?.Value;

            var user = await ApplicationExtensions.Get<User>(userName);

            _context.Add(model);
            _context.SaveChanges();
            var character = _context.Characters.Find(model.Id);

            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = model.Name;
            feed.CharacterId = model.Id;
            feed.TimeStamp = DateTime.Now;
            feed.Addition = 1;
            _context.Add(feed);
            _context.SaveChanges();

            return character;
        }

        public async Task<Character> UpdateAsync(Character model, CancellationToken cancellationToken = new CancellationToken())
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("sub")?.Value;

            var user = await ApplicationExtensions.Get<User>(userName);
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = model.Name;
            feed.CharacterId = model.Id;
            feed.TimeStamp = DateTime.Now;
            feed.Update = 1;
            _context.Add(feed);

            character.Name = model.Name;
            character.JapaneseName = model.JapaneseName;
            character.Age = model.Age;
            character.Gender = model.Gender;
            character.Race = model.Race;
            character.Job = model.Job;
            character.Height = model.Height;
            character.Weight = model.Weight;
            character.Origin = model.Origin;
            character.Description = model.Description;
            _context.SaveChanges();
            return model;
        }

        public async Task<Character> DeleteAsync(Character model, CancellationToken cancellationToken = new CancellationToken())
        {
            var claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst("sub")?.Value;

            var user = await ApplicationExtensions.Get<User>(userName);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = model.Name;
            feed.CharacterId = model.Id;
            feed.TimeStamp = DateTime.Now;
            feed.Deletion = 1;
            feed.StatDeletion = 1;
            _context.Add(feed);

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