using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenRequisitionResponse
    {
        #region Properties

        [JsonProperty("accounts")]
        public List<string> Accounts { get; set; }

        [JsonProperty("account_selection")]
        public bool AccountSelection { get; set; }

        [JsonProperty("agreement")]
        public string Agreement { get; set; }

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("institution_id")]
        public string InstitutionId { get; set; }

        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("redirect")]
        public string Redirect { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        [JsonProperty("ssn")]
        public string Ssn { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("user_language")]
        public string UserLanguage { get; set; }

        #endregion
    }
}