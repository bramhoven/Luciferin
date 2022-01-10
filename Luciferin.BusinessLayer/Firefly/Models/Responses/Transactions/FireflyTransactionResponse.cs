namespace Luciferin.BusinessLayer.Firefly.Models.Responses.Transactions
{
    public class FireflyTransactionResponse : FireflySingleResponseBase<FireflyTransactionAttributes>
    {
        #region Properties

        public override FireflyDataContainer<FireflyTransactionAttributes> Data { get; set; }

        #endregion
    }
}