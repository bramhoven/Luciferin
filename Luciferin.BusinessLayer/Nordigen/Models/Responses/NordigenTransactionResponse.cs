using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenTransactionResponse
    {
        #region Properties

        [JsonProperty("transactions")]
        public NordigenTransactionResponseTransactions Transactions { get; set; }

        #endregion
    }

    public class NordigenTransactionResponseTransactions
    {
        #region Properties

        [JsonProperty("booked")]
        public ICollection<NordigenTransaction> Booked { get; set; }

        [JsonProperty("pending")]
        public ICollection<NordigenTransaction> Pending { get; set; }

        #endregion
    }
}