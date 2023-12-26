namespace Luciferin.Website.Classes.Extensions;

using System;
using BusinessLayer.Settings;
using Infrastructure.Settings;
using Infrastructure.Storage.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public static class ConfigurationBuilderExtensions
{
    /// <summary>
    ///     Add the Luciferin configuration.
    ///     This gets all the Luciferin settings from the database.
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IConfigurationBuilder AddLuciferinConfiguration(this IConfigurationBuilder builder)
    {
        var tempConfig = builder.Build();

        var optionsBuilder = new DbContextOptionsBuilder<StorageContext>();

        var connectionString = tempConfig.GetConnectionString("Luciferin");
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return builder;
        }

        var luciferinSettings = new LuciferinSettings();
        tempConfig.GetSection("Luciferin").Bind(luciferinSettings);
        switch (luciferinSettings.StorageProvider)
        {
            case "mysql":
                var version = ServerVersion.AutoDetect(connectionString);
                optionsBuilder
                    .UseMySql(connectionString, version)
                    .LogTo(Console.WriteLine, LogLevel.Information)
                    .EnableSensitiveDataLogging()
                    .EnableDetailedErrors();
                break;
        }

        return builder.Add(new LuciferinConfigurationSource(optionsBuilder.Options));
    }
}