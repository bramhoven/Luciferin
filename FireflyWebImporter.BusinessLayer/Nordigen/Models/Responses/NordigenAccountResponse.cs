using System;
using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountResponse
    {
        #region Properties

        [JsonProperty("created")]
        public DateTime Created { get; set; }

        [JsonProperty("iban")]
        public string Iban { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("institution_id")]
        public string InstitutionId { get; set; }

        [JsonProperty("last_accessed")]
        public DateTime LastAccessed { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        #endregion
    }
}