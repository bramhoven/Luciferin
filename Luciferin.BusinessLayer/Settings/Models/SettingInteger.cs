using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class SettingInteger : SettingBase<int>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => true;

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.Integer;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public SettingInteger(int id, string name, int value) : base(id, name, value) { }

        #endregion
    }
}