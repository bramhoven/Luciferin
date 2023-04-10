using System;
using Luciferin.BusinessLayer.Configuration;
using Luciferin.BusinessLayer.Configuration.Interfaces;
using Luciferin.BusinessLayer.Converters.Helper;
using Luciferin.BusinessLayer.Firefly;
using Luciferin.BusinessLayer.Firefly.Stores;
using Luciferin.BusinessLayer.Helpers;
using Luciferin.BusinessLayer.Import;
using Luciferin.BusinessLayer.Import.Mappers;
using Luciferin.BusinessLayer.Import.Processors;
using Luciferin.BusinessLayer.Import.Stores;
using Luciferin.BusinessLayer.Jobs;
using Luciferin.BusinessLayer.Logger;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.BusinessLayer.Nordigen.Stores;
using Luciferin.BusinessLayer.Notifications;
using Luciferin.BusinessLayer.ServiceBus;
using Luciferin.BusinessLayer.Settings;
using Luciferin.BusinessLayer.Settings.Enums;
using Luciferin.BusinessLayer.Settings.Stores;
using Luciferin.DataLayer.Mail;
using Luciferin.DataLayer.Storage;
using Luciferin.DataLayer.Storage.Context;
using Luciferin.Website.Classes.Logger;
using Luciferin.Website.Classes.Queue;
using Luciferin.Website.Classes.ServiceBus;
using Luciferin.Website.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Quartz;

namespace Luciferin.Website;

public class Startup
{
    #region Constructors

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        CompositeConfiguration = new Configuration(Configuration);

        LuciferinSettings = new LuciferinSettings();
        Configuration.GetSection("Luciferin").Bind(LuciferinSettings);
    }

    #endregion

    #region Properties

    private ICompositeConfiguration CompositeConfiguration { get; }

    private IConfiguration Configuration { get; }

    private LuciferinSettings LuciferinSettings { get; }

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
            // endpoints.MapControllerRoute("Home", "{controller=Home}/{action=Index}");
            // endpoints.MapControllerRoute("Configuration", "{controller=Configuration}/{action=Index}");
            endpoints.MapControllers();
            endpoints.MapHub<ImporterHub>("hubs/importer");
        });

        MigrateAndSeedDatabases(app);
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();
        services.AddSignalR();
        services.AddRouting(o => o.LowercaseUrls = true);

        services.Configure<LuciferinSettings>(Configuration.GetSection("Luciferin"));
        services.Configure<MailSettings>(Configuration.GetSection("Mail"));

        services.TryAddSingleton(typeof(ICompositeLogger<>), typeof(HubLogger<>));

        services.AddScoped<IServiceBus, HubServiceBus>();

        services.AddHostedService<QueuedHostedService>();
        services.AddSingleton<IBackgroundTaskQueue>(ctx => new BackgroundTaskQueue(1));

        services.AddScoped<ConverterHelper>();
        services.AddScoped<AccountMapper>();
        services.AddScoped<TransactionMapper>();

        services.AddMappers();

        ConfigureStorage(services);

        ConfigureDataLayers(services);
        ConfigureStores(services);
        ConfigureProcessors(services);
        ConfigureManagers(services);
        ConfigureNotifications(services);

        ConfigureQuartz(services);
    }

    private void ConfigureNotifications(IServiceCollection services)
    {
        services.AddScoped<IExpirationNotificationComponent>(factory =>
        {
            var options = factory.GetRequiredService<IOptionsSnapshot<LuciferinSettings>>().Value;
            if (options == null)
                return new NullExpirationNotificationComponent();

            switch (options.NotificationMethod)
            {
                case NotificationMethod.Mail:
                    return new MailExpirationNotificationComponent(factory.GetRequiredService<MailDal>());
                default:
                    return new NullExpirationNotificationComponent();
            }
        });
    }

    private void ConfigureQuartz(IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            q.UseMicrosoftDependencyInjectionJobFactory();
            
            var checkRequisitionExpirationKey = new JobKey("CheckRequisitionExpirationJob");
            q.AddJob<CheckRequisitionExpirationJob>(opts => opts.WithIdentity(checkRequisitionExpirationKey));
            q.AddTrigger(opts => opts
                .ForJob(checkRequisitionExpirationKey)
                .WithIdentity("CheckRequisitionExpirationJob-Trigger")
                .WithCronSchedule("0 0 0 * * ?"));
            
            #if DEBUG
            var checkRequisitionExpirationImmediateKey = new JobKey("CheckRequisitionExpirationImmediateJob");
            q.AddJob<CheckRequisitionExpirationJob>(opts => opts.WithIdentity(checkRequisitionExpirationImmediateKey).StoreDurably());
            q.AddTrigger(opts => opts
                .ForJob(checkRequisitionExpirationKey)
                .WithIdentity("CheckRequisitionExpirationImmediateJob-Trigger")
                .WithSimpleSchedule()
                .StartNow());
            #endif
            
            var importJobKey = new JobKey("ImportJob");
            q.AddJob<ImportJob>(opts => opts.WithIdentity(importJobKey));
            q.AddTrigger(opts => opts
                .ForJob(importJobKey)
                .WithIdentity("ImportJob-Trigger")
                .WithCronSchedule("0 0 */1 * * ?"));
        });
        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
    }

    private void ConfigureStorage(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("Luciferin");
        switch (LuciferinSettings.StorageProvider)
        {
            case "mysql":
                var version = ServerVersion.AutoDetect(connectionString);
                services.AddDbContext<StorageContext>(options =>
                {
                    options
                        .UseMySql(connectionString, version)
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                });
                break;
        }
    }

    private void ConfigureStores(IServiceCollection services)
    {
        services.AddScoped<ISettingsStore, StorageSettingStore>();
        services.AddScoped<IImportStatisticsStore, StorageImportStatisticsStore>();

        services.AddScoped<INordigenStore, NordigenStore>();
        services.AddScoped<IFireflyStore, FireflyStore>();
    }

    private void MigrateAndSeedDatabases(IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
        {
            var storageContext = serviceScope.ServiceProvider.GetRequiredService<StorageContext>();
            storageContext.Database.EnsureCreated();
            storageContext.Database.GetPendingMigrations();
            storageContext.Database.Migrate();

            var settingsDal = serviceScope.ServiceProvider.GetRequiredService<SettingsDal>();
            settingsDal.EnsureSettingsExist();
        }
    }

    #region Static Methods

    private static void ConfigureDataLayers(IServiceCollection services)
    {
        services.AddScoped<SettingsDal>();
        services.AddScoped<MailDal>();
        services.AddScoped<ImportStatisticsDal>();
    }

    private static void ConfigureProcessors(IServiceCollection services)
    {
        services.AddScoped<FilterExistingTransactionProcessor>();
        services.AddScoped<FilterDuplicateTransactionProcessor>();
    }

    private static void ConfigureManagers(IServiceCollection services)
    {
        services.AddScoped<ISettingsManager, SettingsManager>();
        services.AddScoped<INordigenManager, NordigenManager>();
        services.AddScoped<IFireflyManager, FireflyManager>();
        services.AddScoped<IImportManager, ImportManager>();
        // services.AddScoped<IImportManager, TestImportManager>();
    }

    #endregion

    #endregion
}