using Microsoft.EntityFrameworkCore;

namespace Mog.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Mog.Models.Character> Characters { get; set; } 
        public DbSet<Mog.Models.Monster> Monsters { get; set; }  
        public DbSet<Mog.Models.Game> Games { get; set; }  
    }
}
