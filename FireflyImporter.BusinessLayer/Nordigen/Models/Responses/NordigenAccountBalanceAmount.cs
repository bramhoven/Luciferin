using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountBalanceAmount
    {
        #region Properties

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        #endregion
    }
}