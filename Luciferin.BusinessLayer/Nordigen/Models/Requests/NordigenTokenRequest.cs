using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenTokenRequest
    {
        #region Properties

        [JsonPropertyName("secret_id")]
        public string SecretId { get; set; }

        [JsonPropertyName("secret_key")]
        public string SecretKey { get; set; }

        #endregion
    }
}