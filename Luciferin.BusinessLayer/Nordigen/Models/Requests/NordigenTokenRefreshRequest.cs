using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenTokenRefreshRequest
    {
        #region Properties

        [JsonProperty("refresh")]
        public string Refresh { get; set; }

        #endregion
    }
}