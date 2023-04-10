using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class IntegerSetting : SettingBase<int>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => true;

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.Integer;

        #endregion

        #region Constructors

        public IntegerSetting() { }

        /// <inheritdoc />
        public IntegerSetting(string category, string name, int value) : base(category, name, value) { }

        #endregion
    }
}