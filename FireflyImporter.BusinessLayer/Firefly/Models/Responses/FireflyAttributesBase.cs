using System;
using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyAttributesBase
    {
        #region Properties

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        #endregion
    }
}