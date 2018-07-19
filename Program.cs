using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Moogle.Data;
using Moogle.Models;

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
				CreateRoles(provider, Startup.Configuration);
				provider.GetService<CharacterContext>().Database.Migrate();
				provider.GetService<ApplicationDbContext>().Database.Migrate();
            }

            host.Run();
        }
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void CreateRoles(IServiceProvider serviceProvider, IConfiguration Configuration)
        {
            const string adminRoleName = "Admin";
            string[] roleNames = { adminRoleName, "Manager", "Member" };

            foreach (string roleName in roleNames)
            {
                CreateRole(serviceProvider, roleName);
            }

            // Setting up admin user
            string adminUser = Configuration["AdminUser"];
            string adminPassword = Configuration["AdminPassword"];
            
            // Setting up test user for mentors
            string testUser = Configuration["TestUser"];
            string testPassword = Configuration["TestPassword"];
            
            AddUserToRole(serviceProvider, adminUser, adminPassword, adminRoleName);
            AddUserToRole(serviceProvider, testUser, testPassword, "Member");
        }

        private static void CreateRole(IServiceProvider serviceProvider, string roleName)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            Task<bool> roleExists = roleManager.RoleExistsAsync(roleName);
            roleExists.Wait();

            if (!roleExists.Result)
            {
                Task<IdentityResult> roleResult = roleManager.CreateAsync(new IdentityRole(roleName));
                roleResult.Wait();
            }
        }
        
        private static void AddUserToRole(IServiceProvider serviceProvider, string userEmail, 
            string userPwd, string roleName)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            Task<ApplicationUser> checkAppUser = userManager.FindByEmailAsync(userEmail);
            checkAppUser.Wait();

            ApplicationUser appUser = checkAppUser.Result;

            if (checkAppUser.Result == null)
            {
                ApplicationUser newAppUser = new ApplicationUser
                {
                    Email = userEmail,
                    UserName = userEmail
                };

                Task<IdentityResult> taskCreateAppUser = userManager.CreateAsync(newAppUser, userPwd);
                taskCreateAppUser.Wait();

                if (taskCreateAppUser.Result.Succeeded)
                {
                    appUser = newAppUser;
                }
            }

            Task<IdentityResult> newUserRole = userManager.AddToRoleAsync(appUser, roleName);
            newUserRole.Wait();
        }
    }
}
