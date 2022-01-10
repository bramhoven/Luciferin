using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenTokenRequest
    {
        #region Properties

        [JsonProperty("secret_id")]
        public string SecretId { get; set; }

        [JsonProperty("secret_key")]
        public string SecretKey { get; set; }

        #endregion
    }
}