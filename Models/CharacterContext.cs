using Microsoft.EntityFrameworkCore;  

namespace Moogle.Models  
{  
    public class CharacterContext : DbContext  
    {  
        public CharacterContext (DbContextOptions<CharacterContext> options)  
            : base(options)  
        {  
        }  
        public DbSet<Moogle.Models.Characters> Character { get; set; } // I did this one backwards
        public DbSet<Moogle.Models.Monster> Monsters { get; set; }  
        public DbSet<Moogle.Models.Game> Games { get; set; }  

    }  
}