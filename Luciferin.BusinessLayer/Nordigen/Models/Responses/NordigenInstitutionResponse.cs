using System.Collections.Generic;
using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenInstitutionResponse
    {
        #region Properties

        [JsonProperty("bic")]
        public string Bic { get; set; }

        [JsonProperty("countries")]
        public ICollection<string> Countries { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("logo")]
        public string Logo { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("transaction_total_days")]
        public int TransactionTotalDays { get; set; }

        #endregion
    }
}