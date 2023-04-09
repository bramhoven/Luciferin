using System;
using ValueType = Luciferin.BusinessLayer.Settings.Enums.ValueType;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class TimeSpanSetting : SettingBase<TimeSpan>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => true;

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.TimeSpan;

        #endregion

        #region Constructors

        public TimeSpanSetting() { }

        /// <inheritdoc />
        public TimeSpanSetting(string category, string name, TimeSpan value) : base(category, name, value) { }

        #endregion
    }
}