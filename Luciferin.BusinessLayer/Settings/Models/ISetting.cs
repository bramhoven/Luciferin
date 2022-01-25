using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public interface ISetting
    {
        #region Properties

        bool HasValue { get; }

        int Id { get; }

        string Key { get; }

        ValueType ValueType { get; }

        #endregion
    }
}