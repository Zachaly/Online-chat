using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Online_chat.Data;
using Online_chat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Online_chat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var scope = host.Services.CreateScope();

            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                context.Database.EnsureCreated();

                var userRole = new IdentityRole("User");

                if (!context.Roles.Any())
                {
                    roleManager.CreateAsync(userRole).Wait();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"dupa {e.Message}");
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
