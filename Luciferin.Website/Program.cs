using System.Linq;
using Luciferin.Website.Classes.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Luciferin.Website
{
    public class Program
    {
        #region Methods

        #region Static Methods

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builder, options) =>
                {
                    options
                        .AddUserSecrets<Startup>()
                        .AddEnvironmentVariables()
                        .AddLuciferinConfiguration()
                        .Build();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        #endregion

        #endregion
    }
}