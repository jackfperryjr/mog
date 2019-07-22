using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Moogle.Data;

namespace Moogle
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var provider = scope.ServiceProvider;
                // provider.GetService<ApplicationDbContext>().Database.EnsureCreated();
				provider.GetService<ApplicationDbContext>().Database.Migrate();
                Roles.CreateRoles(provider, Startup.Configuration);
                //SeedData.SeedDB(context);
            }
            host.Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}