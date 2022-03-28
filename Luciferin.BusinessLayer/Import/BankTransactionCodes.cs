namespace Luciferin.BusinessLayer.Import
{
    public static class BankTransactionCodes
    {
        #region Fields

        /// <summary>
        /// The nordigen bank transaction code for an authorization.
        /// </summary>
        public const string Authorisation = "PMNT-MCRD-UPCT";

        /// <summary>
        /// The nordigen bank transaction code for a capture.
        /// </summary>
        public const string Capture = "PMNT-CCRD-POSD";

        #endregion
    }
}