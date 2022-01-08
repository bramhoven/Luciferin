using System.Linq;
using FireflyImporter.BusinessLayer.Configuration;
using FireflyImporter.BusinessLayer.Configuration.Interfaces;
using FireflyImporter.BusinessLayer.Firefly;
using FireflyImporter.BusinessLayer.Firefly.Stores;
using FireflyImporter.BusinessLayer.Hubs;
using FireflyImporter.BusinessLayer.Import;
using FireflyImporter.BusinessLayer.Logger;
using FireflyImporter.BusinessLayer.Nordigen;
using FireflyImporter.BusinessLayer.Nordigen.Stores;
using FireflyImporter.Website.Classes.Logger;
using FireflyImporter.Website.Classes.Queue;
using FireflyImporter.Website.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FireflyImporter.Website
{
    public class Startup
    {
        #region Properties

        private ICompositeConfiguration CompositeConfiguration { get; }

        private IConfiguration Configuration { get; }

        #endregion

        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            CompositeConfiguration = new Configuration(Configuration);
        }

        #endregion

        #region Methods

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("Home", "{controller=Home}/{action=Index}");
                endpoints.MapControllerRoute("Configuration", "{controller=Configuration}/{action=Index}");
                endpoints.MapHub<ImporterHub>("hubs/importer");
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddSignalR();
            
            services.AddScoped<ICompositeLogger>(s => new HubLogger(s.GetRequiredService<IHubContext<ImporterHub, IImporterHub>>()));

            services.AddHostedService<QueuedHostedService>();
            services.AddSingleton<IBackgroundTaskQueue>(ctx => new BackgroundTaskQueue(1));

            services.AddScoped<ICompositeConfiguration>(s => new Configuration(Configuration));
            services.AddScoped<INordigenConfiguration>(s => s.GetRequiredService<ICompositeConfiguration>());
            services.AddScoped<IFireflyConfiguration>(s => s.GetRequiredService<ICompositeConfiguration>());
            services.AddScoped<IImportConfiguration>(s => s.GetRequiredService<ICompositeConfiguration>());

            services.AddScoped<INordigenStore>(s => new NordigenStore(CompositeConfiguration.NordigenBaseUrl, CompositeConfiguration.NordigenSecretId, CompositeConfiguration.NordigenSecretKey));
            services.AddScoped<INordigenManager, NordigenManager>();

            services.AddScoped<IFireflyStore>(s => new FireflyStore(CompositeConfiguration.FireflyBaseUrl, CompositeConfiguration.FireflyAccessToken, (ILogger<FireflyStore>)s.GetService(typeof(ILogger<FireflyStore>))));
            services.AddScoped<IFireflyManager, FireflyManager>();

            services.AddScoped<IImportManager, ImportManager>();
            // services.AddScoped<IImportManager, TestImportManager>();
        }

        #endregion
    }
}