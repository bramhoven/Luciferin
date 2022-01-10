using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses
{
    public abstract class FireflyCollectionResponseBase<TAttributes> : FireflyResponseBase<TAttributes>
        where TAttributes : FireflyAttributesBase
    {
        #region Properties
        
        [JsonProperty("data")]
        public abstract ICollection<FireflyDataContainer<TAttributes>> Data { get; set; }

        #endregion
    }
}