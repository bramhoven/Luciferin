namespace Luciferin.Core.Exceptions;

public class SettingNotFoundException : Exception
{
    #region Constructors

    public SettingNotFoundException()
    {
    }

    public SettingNotFoundException(string message) : base(message)
    {
    }

    public SettingNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }

    #endregion
}