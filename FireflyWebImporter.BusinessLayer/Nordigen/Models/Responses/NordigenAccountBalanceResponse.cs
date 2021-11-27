using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountBalanceResponse
    {
        #region Properties

        [JsonProperty("balances")]
        public ICollection<NordigenAccountBalance> Balances { get; set; }

        #endregion
    }
}