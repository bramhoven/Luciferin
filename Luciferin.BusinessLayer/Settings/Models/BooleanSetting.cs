using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class BooleanSetting : SettingBase<bool>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => true;

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.Boolean;
        
        #endregion

        #region Constructors
        
        public BooleanSetting() {}

        /// <inheritdoc />
        public BooleanSetting(string category, string name, bool value) : base(category, name, value) { }

        #endregion
    }
}