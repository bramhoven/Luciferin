using System;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountBalance
    {
        #region Properties

        [JsonProperty("balanceAmount")]
        public BalanceAmount BalanceAmount { get; set; }

        [JsonProperty("balanceType")]
        public string BalanceType { get; set; }

        [JsonProperty("lastChangeDateTime")]
        public DateTime LastChangeDateTime { get; set; }

        #endregion
    }
}