using Luciferin.BusinessLayer.Settings.Enums;

namespace Luciferin.BusinessLayer.Settings.Models
{
    public abstract class SettingBase<TDataType> : ISettingValue<TDataType>
    {
        #region Properties

        /// <inheritdoc />
        public abstract bool HasValue { get; }

        /// <inheritdoc />
        public int Id { get; }

        /// <inheritdoc />
        public string Name { get; }

        /// <inheritdoc />
        public TDataType Value { get; set; }

        /// <inheritdoc />
        public abstract ValueType ValueType { get; }

        #endregion

        #region Constructors

        protected SettingBase(int id, string name, TDataType value)
        {
            Id = id;
            Name = name;
            Value = value;
        }

        #endregion
    }
}