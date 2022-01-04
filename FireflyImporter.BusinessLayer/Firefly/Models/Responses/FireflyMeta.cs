using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Firefly.Models.Responses
{
    public class FireflyMeta
    {
        #region Properties

        [JsonProperty("pagination")]
        public FireflyPagination Pagination { get; set; }

        #endregion
    }
}