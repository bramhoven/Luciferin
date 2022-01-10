using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenPaginatedResponse<TNordigenData>
    {
        #region Properties

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("previous")]
        public string Previous { get; set; }

        [JsonProperty("results")]
        public ICollection<TNordigenData> Results { get; set; }

        #endregion
    }
}