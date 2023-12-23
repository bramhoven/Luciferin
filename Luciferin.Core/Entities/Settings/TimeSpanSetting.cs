namespace Luciferin.Core.Entities.Settings;

using Enums;

public class TimeSpanSetting : SettingBase<TimeSpan?>
{
    public TimeSpanSetting() { }

    /// <inheritdoc />
    public TimeSpanSetting(string category, string name, TimeSpan? value) : base(category, name, value) { }

    /// <inheritdoc />
    public override bool HasValue => true;

    /// <inheritdoc />
    public override ValueTypeEnum ValueType => ValueTypeEnum.TimeSpan;
}