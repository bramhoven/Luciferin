namespace Luciferin.Core.Entities.Settings;

using Enums;

public class StringSetting : SettingBase<string>
{
    public StringSetting() { }

    /// <inheritdoc />
    public StringSetting(string category, string name, string value) : base(category, name, value) { }

    /// <inheritdoc />
    public override bool HasValue => !string.IsNullOrWhiteSpace(Value);

    /// <inheritdoc />
    public override ValueTypeEnum ValueType => ValueTypeEnum.String;
}