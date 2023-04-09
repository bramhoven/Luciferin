using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public abstract class SettingBase<TDataType> : ISettingValue<TDataType>
    {
        #region Properties

        /// <inheritdoc />
        public abstract bool HasValue { get; }

        /// <inheritdoc />
        public string Key { get; set; }

        /// <inheritdoc />
        public string Category { get; }

        /// <inheritdoc />
        public TDataType Value { get; set; }

        /// <inheritdoc />
        public abstract ValueType ValueType { get; }

        #endregion

        #region Constructors

        protected SettingBase() { }

        protected SettingBase(string category, string name, TDataType value)
        {
            Category = category;
            Key = name;
            Value = value;
        }

        #endregion
    }
}