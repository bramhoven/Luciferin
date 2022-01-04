using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Nordigen.Models
{
    public class BalanceAmount
    {
        #region Properties

        [JsonProperty("amount")]
        public string Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        #endregion
    }
}