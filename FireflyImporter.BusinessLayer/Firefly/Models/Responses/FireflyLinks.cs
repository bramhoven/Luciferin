using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyLinks
    {
        #region Properties

        [JsonProperty("first")]
        public string First { get; set; }

        [JsonProperty("last")]
        public string Last { get; set; }

        [JsonProperty("self")]
        public string Self { get; set; }

        #endregion
    }
}