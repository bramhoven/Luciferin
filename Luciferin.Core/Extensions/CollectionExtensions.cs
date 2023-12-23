namespace Luciferin.Core.Extensions;

using Abstractions;
using Exceptions;

public static class CollectionExtensions
{
    /// <summary>
    ///     Gets the typed setting from a settings collection.
    /// </summary>
    /// <param name="settings">The settings collection.</param>
    /// <param name="category">The setting category.</param>
    /// <param name="key">The setting key.</param>
    /// <typeparam name="TSettingType">The setting type.</typeparam>
    /// <returns></returns>
    public static TSettingType GetSetting<TSettingType>(this IEnumerable<ISetting> settings, string category, string key)
        where TSettingType : ISetting
    {
        var setting = settings.FirstOrDefault(s =>
            string.Equals(s.Category, category, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(s.Key, key, StringComparison.InvariantCultureIgnoreCase));
        if (setting == null)
        {
            throw new SettingNotFoundException($"Setting not found with key: {key}");
        }

        return (TSettingType)setting;
    }
}