namespace Luciferin.Website.Classes.Extensions;

using System;
using BusinessLayer.Jobs;
using Infrastructure.Mail;
using Infrastructure.Settings;
using Infrastructure.Storage.Context;
using Infrastructure.Storage.Mysql;
using Infrastructure.Storage.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;

public static class ServiceCollectionExtensions
{
    public static void AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<LuciferinSettings>(configuration.GetSection("Luciferin"));
        services.Configure<MailSettings>(configuration.GetSection("Mail"));
        services.Configure<FireflySettings>(configuration.GetSection("Firefly"));
        services.Configure<GoCardlessSettings>(configuration.GetSection("GoCardless"));
    }

    public static void AddDbContext(this IServiceCollection services, IConfiguration configuration, LuciferinSettings luciferinSettings)
    {
        var connectionString = configuration.GetConnectionString("Luciferin");
        switch (luciferinSettings.StorageProvider)
        {
            case "mysql":
                var version = ServerVersion.AutoDetect(connectionString);
                services.AddDbContext<StorageContext>(options =>
                {
                    options
                        .UseMySql(connectionString, version,
                            ctx => ctx.MigrationsAssembly(typeof(MysqlMigrations).Assembly.FullName))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                });
                break;
            case "postgres":
                services.AddDbContext<StorageContext>(options =>
                {
                    options
                        .UseNpgsql(connectionString,
                            ctx => ctx.MigrationsAssembly(typeof(PostgresMigrations).Assembly.FullName))
                        .LogTo(Console.WriteLine, LogLevel.Information)
                        .EnableSensitiveDataLogging()
                        .EnableDetailedErrors();
                });
                AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
                break;
        }
    }

    public static void AddQuartzJobs(this IServiceCollection services)
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
            q.AddJob<CheckRequisitionExpirationJob>(opts =>
                opts.WithIdentity(checkRequisitionExpirationImmediateKey).StoreDurably());
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
}