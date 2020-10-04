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
    public class StatStore : IStore<Stat>
    {
        private ApplicationDbContext _context;
        private IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public StatStore(
            ApplicationDbContext context,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Stat> AddAsync(Stat model, CancellationToken cancellationToken = new CancellationToken())
        {
            var user = await ApplicationExtensions.Get<User>($"jackfperryjr");
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = character.Name;
            feed.CharacterId = character.Id;
            feed.TimeStamp = DateTime.Now;
            feed.StatAddition = 1;
            _context.Add(feed);

            _context.Add(model);
            _context.SaveChanges();
            return model;
        }

        public async Task<Stat> UpdateAsync(Stat model, CancellationToken cancellationToken = new CancellationToken())
        {
            var stat = await _context.Stats.FirstOrDefaultAsync(x => x.Id == model.Id);
            var user = await ApplicationExtensions.Get<User>($"jackfperryjr");
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = character.Name;
            feed.CharacterId = character.Id;
            feed.TimeStamp = DateTime.Now;
            feed.StatUpdate = 1;
            _context.Add(feed);

            stat.Platform = model.Platform;
            stat.Class = model.Class;
            stat.Level = model.Level;
            stat.HitPoints = model.HitPoints;
            stat.ManaPoints = model.ManaPoints;
            stat.Attack = model.Attack;
            stat.Defense = model.Defense;
            stat.Magic = model.Magic;
            stat.MagicDefense = model.MagicDefense;
            stat.Agility = model.Agility;
            stat.Spirit = model.Spirit;
            _context.SaveChanges();
            return model;
        }

        public async Task<Stat> DeleteAsync(Stat model, CancellationToken cancellationToken = new CancellationToken())
        {
            var stat = await _context.Stats.FirstOrDefaultAsync(x => x.Id == model.Id);
            var user = await ApplicationExtensions.Get<User>($"jackfperryjr");
            var character = await _context.Characters.FirstOrDefaultAsync(x => x.Id == model.Id);
            var feed = new Feed();
            feed.UserName = user.UserName;
            feed.UserFirstName = user.FirstName;
            feed.UserPhoto = user.Photo;
            feed.CharacterName = character.Name;
            feed.CharacterId = character.Id;
            feed.TimeStamp = DateTime.Now;
            feed.StatDeletion = 1;
            _context.Add(feed);

            _context.Stats.Remove(stat);
            _context.SaveChanges();
            return model;
        }
    }
}