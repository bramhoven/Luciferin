namespace Luciferin.Website.Classes.Settings;

using System.Collections.Generic;
using Core.Abstractions;
using Core.Entities.Settings;
using Core.Extensions;

public static class PlatformSettingsMapper
{
    #region Methods

    #region Static Methods

    /// <summary>
    ///     Maps a collection of ISetting to the PlatformSettings model.
    /// </summary>
    /// <param name="settings">The settings collection.</param>
    /// <returns></returns>
    public static PlatformSettings MapSettingsCollectionToPlatformSettings(ICollection<ISetting> settings)
    {
        return new PlatformSettings
        {
            FireflyUrl = settings.GetSetting<StringSetting>(SettingCategoryConstants.Firefly, SettingKeyConstants.FireflyUrlKey),
            FireflyAccessToken = settings.GetSetting<StringSetting>(SettingCategoryConstants.Firefly, SettingKeyConstants.FireflyAccessTokenKey),
            GoCardlessBaseUrl = settings.GetSetting<StringSetting>(SettingCategoryConstants.GoCardless, SettingKeyConstants.GoCardlessBaseUrlKey),
            GoCardlessSecretId = settings.GetSetting<StringSetting>(SettingCategoryConstants.GoCardless, SettingKeyConstants.GoCardlessSecretIdKey),
            GoCardlessSecretKey = settings.GetSetting<StringSetting>(SettingCategoryConstants.GoCardless, SettingKeyConstants.GoCardlessSecretKeyKey),
            ImportDays = settings.GetSetting<IntegerSetting>(SettingCategoryConstants.Luciferin, SettingKeyConstants.ImportDaysKey),
            ExtendedNotes = settings.GetSetting<BooleanSetting>(SettingCategoryConstants.Luciferin, SettingKeyConstants.ExtendedNotesKey),
            AutomaticImport = settings.GetSetting<BooleanSetting>(SettingCategoryConstants.Luciferin, SettingKeyConstants.AutomaticImportKey),
            FilterAuthorisations = settings.GetSetting<BooleanSetting>(SettingCategoryConstants.Luciferin, SettingKeyConstants.FilterAuthorisations),
            NotificationEmail = settings.GetSetting<StringSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailNotificationEmail),
            FromEmail = settings.GetSetting<StringSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailFromEmail),
            Host = settings.GetSetting<StringSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailHost),
            Port = settings.GetSetting<IntegerSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailPort),
            Username = settings.GetSetting<StringSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailUsername),
            Password = settings.GetSetting<StringSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailPassword),
            EnableSsl = settings.GetSetting<BooleanSetting>(SettingCategoryConstants.Mail, SettingKeyConstants.MailEnableSsl)
        };
    }

    #endregion

    #endregion
}