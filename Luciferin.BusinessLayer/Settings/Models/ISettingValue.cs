namespace Luciferin.BusinessLayer.Settings.Models
{
    public interface ISettingValue<TDataType> : ISetting
    {
        #region Properties

        TDataType Value { get; set; }

        #endregion
    }
}