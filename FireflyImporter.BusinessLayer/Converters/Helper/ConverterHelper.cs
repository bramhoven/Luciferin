using FireflyImporter.BusinessLayer.Converters.Enums;

namespace FireflyImporter.BusinessLayer.Converters.Helper
{
    public static class ConverterHelper
    {
        #region Methods

        #region Static Methods

        /// <summary>
        /// Gets a transaction converter based on bank type.
        /// </summary>
        /// <param name="bankType">The bank type.</param>
        /// <returns></returns>
        public static ITransactionConverter GetTransactionConverter(BankType bankType)
        {
            switch (bankType)
            {
                case BankType.ING:
                    return new INGConverter();
                case BankType.SNS:
                    return new SNSConverter();
                case BankType.N26:
                    return new N26Converter();
                default:
                    return new DefaultConverter();
            }
        }

        #endregion

        #endregion
    }
}