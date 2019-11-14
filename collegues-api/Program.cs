using System;
using ColleguesApi.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Resources;
using System.Reflection;
using System.Globalization;

namespace ColleguesApi
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<ColleguesContext>();
                context.Database.EnsureCreated();
            }
            catch (InvalidOperationException ex)
            {
                ResourceManager rm = new ResourceManager("fr-FR", Assembly.GetExecutingAssembly());
                ILogger<Program> logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, rm.GetString("An error occurred creating the DB.", CultureInfo.CurrentCulture));
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureLogging(o => o.AddDebug()).UseStartup<Startup>();
                });
    }
}