using System.Collections.Generic;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Settings;

public class PlatformSettings
{
    #region Properties

    public BooleanSetting AutomaticImport { get; set; }

    public BooleanSetting ExtendedNotes { get; set; }

    public BooleanSetting FilterAuthorisations { get; set; }

    public StringSetting FireflyAccessToken { get; set; }

    public StringSetting FireflyUrl { get; set; }

    public IntegerSetting ImportDays { get; set; }

    public StringSetting GoCardlessBaseUrl { get; set; }

    public StringSetting GoCardlessSecretId { get; set; }

    public StringSetting GoCardlessSecretKey { get; set; }
    
    public StringSetting NotificationEmail { get; set; }

    public StringSetting FromEmail { get; set; }

    public StringSetting Host { get; set; }

    public IntegerSetting Port { get; set; }

    public StringSetting Username { get; set; }

    public StringSetting Password { get; set; }

    public BooleanSetting EnableSsl { get; set; }


    public ICollection<ISetting> Settings => new List<ISetting>
    {
        AutomaticImport, ExtendedNotes, FireflyAccessToken, FireflyUrl, ImportDays, GoCardlessBaseUrl, GoCardlessSecretId,
        GoCardlessSecretKey, FilterAuthorisations, NotificationEmail, FromEmail, Host, Port, Username, Password, EnableSsl
    };

    #endregion
}