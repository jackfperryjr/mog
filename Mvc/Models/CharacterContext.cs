using Microsoft.EntityFrameworkCore;  

namespace Mvc.Models  
{  
    public class CharacterContext : DbContext  
    {  
        public CharacterContext (DbContextOptions<CharacterContext> options)  
            : base(options)  
        {  
        }  
        public DbSet<Mvc.Models.Characters> Character { get; set; } // I did this one backwards
        // public DbSet<Mvc.Models.Monster> Monsters { get; set; }  
        // public DbSet<Mvc.Models.Game> Games { get; set; }  

    }  
}