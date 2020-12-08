using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RecipeManager.Data;
using RecipeManager.Models;

namespace RecipeManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var configuration = host.Services.GetService<IConfiguration>();
            var hosting = host.Services.GetService<IWebHostEnvironment>();

            if (hosting.IsDevelopment())
            {
                var secrets = configuration.GetSection("Secrets").Get<AppSecrets>();
                DbInitializer.appSecrets = secrets;
            }

            using (var scope = host.Services.CreateScope()) {
                DbInitializer.SeedUsersAndRoles(scope.ServiceProvider).Wait();
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
