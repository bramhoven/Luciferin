namespace Luciferin.Core.Abstractions
{
    public interface ISettingValue<TDataType> : ISetting
    {
        #region Properties

        TDataType Value { get; set; }

        #endregion
    }
}