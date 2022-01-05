using System;
using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Shared
{
    public class FireflyApiTag
    {
        #region Properties

        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Created { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("latitude")]
        public long Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("tag")]
        public string Tag { get; set; }

        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public string Updated { get; set; }

        [JsonProperty("zoom_level")]
        public int ZoomLevel { get; set; }

        #endregion
    }
}