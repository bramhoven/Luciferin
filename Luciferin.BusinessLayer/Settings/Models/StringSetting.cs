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

        /// <inheritdoc />
        public StringSetting(int id, string name, string value) : base(id, name, value) { }

        #endregion
    }
}