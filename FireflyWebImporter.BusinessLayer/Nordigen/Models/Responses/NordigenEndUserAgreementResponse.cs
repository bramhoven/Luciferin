using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenEndUserAgreementResponse
    {
        #region Properties

        [JsonProperty("accepted")]
        public DateTime? Accepted { get; set; }

        [JsonProperty("access_scope")]
        public List<string> AccessScope { get; set; }

        [JsonProperty("access_valid_for_days")]
        public int AccessValidForDays { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("institution_id")]
        public string InstitutionId { get; set; }

        [JsonProperty("max_historical_days")]
        public int MaxHistoricalDays { get; set; }

        #endregion
    }
}