using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Requests
{
    using System.Text.Json.Serialization;

    public class NordigenEndUserAgreementRequest
    {
        #region Properties

        [JsonPropertyName("access_scope")]
        public ICollection<string> AccessScopes { get; set; }

        [JsonPropertyName("access_valid_for_days")]
        public int AccessValidForDays { get; set; }

        [JsonPropertyName("institution_id")]
        public string InstitutionId { get; set; }

        [JsonPropertyName("max_historical_days")]
        public int MaxHistoricalDays { get; set; }

        #endregion
    }
}