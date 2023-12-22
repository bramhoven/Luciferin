using System;
using System.ComponentModel.DataAnnotations;

namespace Luciferin.Infrastructure.Storage.Entities
{
    public class ImportStatistic
    {
        #region Properties

        public int ExistingTransactionsFiltered { get; set; }

        [Key]
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