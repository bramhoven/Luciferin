using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenEndUserAgreementRequest
    {
        #region Properties

        [JsonProperty("access_scope")]
        public ICollection<string> AccessScopes { get; set; }

        [JsonProperty("access_valid_for_days")]
        public int AccessValidForDays { get; set; }

        [JsonProperty("institution_id")]
        public string InstitutionId { get; set; }

        [JsonProperty("max_historical_days")]
        public int MaxHistoricalDays { get; set; }

        #endregion
    }
}