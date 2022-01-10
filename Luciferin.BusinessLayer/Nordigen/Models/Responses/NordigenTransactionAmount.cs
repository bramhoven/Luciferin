using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenTransactionAmount
    {
        #region Properties

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        #endregion
    }
}