namespace Luciferin.Core.Entities.Settings;

using Enums;

public class BooleanSetting : SettingBase<bool?>
{
    public BooleanSetting() { }

    /// <inheritdoc />
    public BooleanSetting(string category, string name, bool? value) : base(category, name, value) { }

    /// <inheritdoc />
    public override bool HasValue => true;

    /// <inheritdoc />
    public override ValueTypeEnum ValueType => ValueTypeEnum.Boolean;
}