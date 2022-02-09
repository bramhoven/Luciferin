using Luciferin.BusinessLayer.Configuration.Interfaces;
using Luciferin.BusinessLayer.Converters.Enums;
using Luciferin.BusinessLayer.Settings;
using Luciferin.BusinessLayer.Settings.Models;

namespace Luciferin.BusinessLayer.Converters.Helper
{
    public class ConverterHelper
    {
        #region Fields

        private readonly ISettingsManager _settingsManager;

        #endregion

        #region Constructors

        public ConverterHelper(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a transaction converter based on bank type.
        /// </summary>
        /// <param name="bankType">The bank type.</param>
        /// <returns></returns>
        public ITransactionConverter GetTransactionConverter(BankType bankType)
        {
            switch (bankType)
            {
                case BankType.ING:
                    return new IngConverter(_settingsManager);
                case BankType.SNS:
                    return new SnsConverter(_settingsManager);
                case BankType.N26:
                    return new N26Converter(_settingsManager);
                default:
                    return new DefaultConverter(_settingsManager);
            }
        }

        #endregion
    }
}