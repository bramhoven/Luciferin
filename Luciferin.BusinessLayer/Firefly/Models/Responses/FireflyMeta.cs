using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyMeta
    {
        #region Properties

        [JsonProperty("pagination")]
        public FireflyPagination Pagination { get; set; }

        #endregion
    }
}