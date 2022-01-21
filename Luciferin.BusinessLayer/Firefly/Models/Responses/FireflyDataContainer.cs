using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyDataContainer<TAttributes>
        where TAttributes : FireflyAttributesBase
    {
        #region Properties

        [JsonProperty("attributes")]
        public TAttributes Attributes { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("links")]
        public FireflyLinks Links { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        #endregion
    }
}