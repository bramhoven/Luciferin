using FireflyWebImporter.BusinessLayer.Converters.Enums;

namespace FireflyWebImporter.BusinessLayer.Converters.Helper
{
    public static class BankHelper
    {
        #region Methods

        #region Static Methods

        public static BankType GetBankType(string institutionId)
        {
            switch (institutionId)
            {
                case "ING_INGBNL2A":
                    return BankType.ING;
                case "SNS_BANK_SNSBNL2A":
                    return BankType.SNS;
                case "N26_NTSBDEB1":
                    return BankType.N26;
                default:
                    return BankType.UNKNOWN;
            }
        }

        #endregion

        #endregion
    }
}