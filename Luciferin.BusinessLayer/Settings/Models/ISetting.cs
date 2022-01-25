using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public interface ISetting
    {
        #region Properties

        bool HasValue { get; }

        int Id { get; }

        string Name { get; }

        ValueType ValueType { get; }

        #endregion
    }
}