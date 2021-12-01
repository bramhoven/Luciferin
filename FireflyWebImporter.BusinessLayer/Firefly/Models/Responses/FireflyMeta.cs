using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyMeta
    {
        #region Properties

        [JsonProperty("pagination")]
        public FireflyPagination Pagination { get; set; }

        #endregion
    }
}