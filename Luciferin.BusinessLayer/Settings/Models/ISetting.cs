using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models;

public interface ISetting
{
    #region Properties

    bool HasValue { get; }

    string Key { get; }

    string Category { get; }

    ValueType ValueType { get; }

    #endregion
}