using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountDetails
    {
        #region Properties

        [JsonProperty("cashAccountType")]
        public string CashAccountType { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("iban")]
        public string Iban { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ownerName")]
        public string OwnerName { get; set; }

        [JsonProperty("product")]
        public string Product { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        #endregion
    }
}