﻿using Microsoft.EntityFrameworkCore;

namespace Mog.Api.Infrastructure
{
    public class SerahDbContext : DbContext
    {
        public SerahDbContext(DbContextOptions<SerahDbContext> options)
            : base(options)
        { }
        public DbSet<Mog.Api.Core.Models.Character> Characters { get; set; }
        public DbSet<Mog.Api.Core.Models.Monster> Monsters { get; set; }  
        public DbSet<Mog.Api.Core.Models.Game> Games { get; set; }  
        public DbSet<Mog.Api.Core.Models.Picture> Pictures { get; set; }
        public DbSet<Mog.Api.Core.Models.Stat> Stats { get; set; }
        public DbSet<Mog.Api.Core.Models.DatingProfile> DatingProfile { get; set; }
        public DbSet<Mog.Api.Core.Models.DatingResponse> Responses { get; set; }
        public DbSet<Mog.Api.Core.Models.Feed> Feed { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
