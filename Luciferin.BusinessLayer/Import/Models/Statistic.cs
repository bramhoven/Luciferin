using System;

namespace Luciferin.BusinessLayer.Import.Models
{
    public class Statistic
    {
        #region Properties

        public int ExistingTransactionsFiltered { get; set; }

        public DateTime ImportDate { get; set; }

        public int NewTransactions { get; set; }

        public bool StartingBalanceSet { get; set; }

        public int TotalAccounts { get; set; }

        public int TotalFireflyTransactions { get; set; }

        public int TotalRetrievedTransactions { get; set; }

        public int TransfersFiltered { get; set; }

        #endregion
    }
}