using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses
{
    public abstract class FireflyResponseBase<TAttributes>
        where TAttributes : FireflyAttributesBase
    {
        #region Properties
        
        [JsonProperty("data")]
        public abstract ICollection<FireflyDataContainer<TAttributes>> Data { get; set; }
        
        [JsonProperty("meta")]
        public FireflyMeta Meta { get; set; }

        [JsonProperty("links")]
        public FireflyLinks Links { get; set; }

        #endregion
    }
}