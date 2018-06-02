using Microsoft.EntityFrameworkCore;  

namespace Mvc.Models  
{  
    public class CharacterContext : DbContext  
    {  
        public CharacterContext (DbContextOptions<CharacterContext> options)  
            : base(options)  
        {  
        }  
        public DbSet<Mvc.Models.Characters> Character { get; set; }  
    }  
}