using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class SettingBoolean : SettingBase<bool>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => true;

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.Boolean;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public SettingBoolean(int id, string name, bool value) : base(id, name, value) { }

        #endregion
    }
}