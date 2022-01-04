using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenTokenRefreshRequest
    {
        #region Properties

        [JsonProperty("refresh")]
        public string Refresh { get; set; }

        #endregion
    }
}