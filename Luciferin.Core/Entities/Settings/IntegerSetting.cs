namespace Luciferin.Core.Entities.Settings;

using Enums;

public class IntegerSetting : SettingBase<int?>
{
    public IntegerSetting() { }

    /// <inheritdoc />
    public IntegerSetting(string category, string name, int? value) : base(category, name, value) { }

    /// <inheritdoc />
    public override bool HasValue => true;

    /// <inheritdoc />
    public override ValueTypeEnum ValueType => ValueTypeEnum.Integer;
}