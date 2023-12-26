namespace Luciferin.Infrastructure.Settings;

using Enums;

public class LuciferinSettings
{
    public string StorageProvider { get; set; }

    public int ImportDays { get; set; }

    public bool ExtendedNotes { get; set; }

    public bool AutomaticImport { get; set; }

    public bool FilterAuthorisations { get; set; }

    public NotificationMethod NotificationMethod { get; set; }
}