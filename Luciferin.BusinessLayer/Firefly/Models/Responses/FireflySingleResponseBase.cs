using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses
{
    public abstract class FireflySingleResponseBase<TAttributes> : FireflyResponseBase<TAttributes>
        where TAttributes : FireflyAttributesBase
    {
        #region Properties

        [JsonProperty("data")]
        public abstract FireflyDataContainer<TAttributes> Data { get; set; }

        #endregion
    }
}