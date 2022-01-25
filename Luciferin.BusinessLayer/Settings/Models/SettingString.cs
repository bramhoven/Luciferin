using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public class SettingString : SettingBase<string>
    {
        #region Properties

        /// <inheritdoc />
        public override bool HasValue => !string.IsNullOrWhiteSpace(Value);

        /// <inheritdoc />
        public override ValueType ValueType => ValueType.String;

        #endregion

        #region Constructors

        /// <inheritdoc />
        public SettingString(int id, string name, string value) : base(id, name, value) { }

        #endregion
    }
}