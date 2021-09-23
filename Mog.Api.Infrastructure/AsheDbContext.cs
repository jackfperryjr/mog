using Microsoft.EntityFrameworkCore;

namespace Mog.Api.Infrastructure
{
    public class AsheDbContext : DbContext
    {
        public AsheDbContext(DbContextOptions<AsheDbContext> options)
            : base(options)
        { }

        public DbSet<Mog.Api.Core.Models.Blog> Blogs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
