using System;
using ValueType = Luciferin.BusinessLayer.Settings.Enums.ValueType;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class SettingTimeSpan : SettingBase<TimeSpan>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => true;

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.TimeSpan;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public SettingTimeSpan(int id, string name, TimeSpan value) : base(id, name, value) { }

        #endregion
    }
}