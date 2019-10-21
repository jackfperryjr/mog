using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Moogle.Models;

namespace Moogle.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Moogle.Models.Characters> Character { get; set; } // I did this one backwards
        public DbSet<Moogle.Models.Monster> Monsters { get; set; }  
        public DbSet<Moogle.Models.Game> Games { get; set; }  
        
        // protected override void OnModelCreating(ModelBuilder builder)
        // {
        //     base.OnModelCreating(builder);
        //     // Customize the ASP.NET Identity model and override the defaults if needed.
        //     // For example, you can rename the ASP.NET Identity table names and more.
        //     // Add your customizations after calling base.OnModelCreating(builder);
        // }
    }
}
