using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Mog.Api.Core.Logging;

namespace Mog.Api.Core.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost RegisterDefaultJson(this IWebHost webHost)
        {
            var settings = webHost.Services.GetService<JsonSerializerSettings>();
            JsonConvert.DefaultSettings = () => settings;
            return webHost;
        }

        public static IWebHost MigrateDatabase<T>(this IWebHost webHost) where T: DbContext
        {
            var environment = webHost.Services.GetService<IWebHostEnvironment>();
            if (!environment.IsDevelopment()) 
                return webHost;

            try
            {
                var serviceScopeFactory = webHost.Services.GetRequiredService<IServiceScopeFactory>();
                using (var scope = serviceScopeFactory.CreateScope())
                using (var context = scope.ServiceProvider.GetRequiredService<T>())
                {
                    if (context.Database.GetPendingMigrations().Any())
                    {
                        context.Database.Migrate();
                    }
                }
            }
            catch
            {
                if (DebugSettings.IsDebugging)
                {
                    throw;
                }
            }

            return webHost;
        }
    }
}