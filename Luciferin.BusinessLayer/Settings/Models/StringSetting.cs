using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class StringSetting : SettingBase<string>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => !string.IsNullOrWhiteSpace(Value);

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.String;

        #endregion

        #region Constructors

        public StringSetting() { }

        /// <inheritdoc />
        public StringSetting(string category, string name, string value) : base(category, name, value) { }

        #endregion
    }
}