using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyDataContainer<TAttributes>
        where TAttributes : FireflyAttributesBase
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("attributes")]
        public TAttributes Attributes { get; set; }
    }
}