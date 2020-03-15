using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mog.Models;

namespace Mog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Mog.Models.Character> Characters { get; set; } // I did this one backwards
        public DbSet<Mog.Models.Monster> Monsters { get; set; }  
        public DbSet<Mog.Models.Game> Games { get; set; }  
        public DbSet<Mog.Models.Picture> Pictures { get; set; }
        public DbSet<Mog.Models.Stat> Stats { get; set; }
        public DbSet<Mog.Models.DatingProfile> DatingProfile { get; set; }
        public DbSet<Mog.Models.DatingResponse> Responses { get; set; }
        
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);
        //     // Customize the ASP.NET Identity model and override the defaults if needed.
        //     // For example, you can rename the ASP.NET Identity table names and more.
        //     // Add your customizations after calling base.OnModelCreating(builder);
        // }
    }
}
