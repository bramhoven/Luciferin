using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenTokenRequests
    {
        #region Properties

        [JsonProperty("secret_id")]
        public string SecretId { get; set; }

        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }

        #endregion
    }
}