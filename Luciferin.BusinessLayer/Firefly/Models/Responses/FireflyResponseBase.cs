using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses
{
    public abstract class FireflyResponseBase<TAttributes>
        where TAttributes : FireflyAttributesBase
    {
        #region Properties
        
        [JsonProperty("links")]
        public FireflyLinks Links { get; set; }

        [JsonProperty("meta")]
        public FireflyMeta Meta { get; set; }

        #endregion
    }
}