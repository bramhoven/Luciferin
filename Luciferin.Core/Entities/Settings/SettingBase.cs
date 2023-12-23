namespace Luciferin.Core.Entities.Settings;

using Abstractions;
using Enums;

public abstract class SettingBase<TDataType> : ISettingValue<TDataType>
{
    protected SettingBase() { }

    protected SettingBase(string category, string name, TDataType value)
    {
        Category = category;
        Key = name;
        Value = value;
    }

    /// <inheritdoc />
    public abstract bool HasValue { get; }

    /// <inheritdoc />
    public string Key { get; set; }

    /// <inheritdoc />
    public string Category { get; }

    /// <inheritdoc />
    public abstract ValueTypeEnum ValueType { get; }

    /// <inheritdoc />
    public TDataType Value { get; set; }
}