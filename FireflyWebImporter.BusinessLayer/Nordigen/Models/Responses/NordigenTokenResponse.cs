using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenTokenResponse
    {
        #region Properties

        [JsonProperty("access")]
        public string Access { get; set; }

        [JsonProperty("access_expires")]
        public int AccessExpires { get; set; }

        [JsonProperty("refresh")]
        public string Refresh { get; set; }

        [JsonProperty("refresh_expires")]
        public int RefreshExpires { get; set; }

        #endregion
    }
}