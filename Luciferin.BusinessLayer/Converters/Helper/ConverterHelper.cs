using Luciferin.BusinessLayer.Configuration.Interfaces;
using Luciferin.BusinessLayer.Converters.Enums;

namespace Luciferin.BusinessLayer.Converters.Helper
{
    public class ConverterHelper
    {
        #region Fields

        private readonly ICompositeConfiguration _configuration;

        #endregion

        #region Constructors

        public ConverterHelper(ICompositeConfiguration configuration)
        {
            _configuration = configuration;
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
                    return new IngConverter(_configuration);
                case BankType.SNS:
                    return new SnsConverter(_configuration);
                case BankType.N26:
                    return new N26Converter(_configuration);
                default:
                    return new DefaultConverter(_configuration);
            }
        }

        #endregion
    }
}