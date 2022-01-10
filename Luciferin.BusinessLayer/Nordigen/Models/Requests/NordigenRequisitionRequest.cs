using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Requests
{
    public class NordigenRequisitionRequest
    {
        #region Properties

        [JsonProperty("agreement")]
        public string AgreementId { get; set; }

        [JsonProperty("institution_id")]
        public string InstitutionId { get; set; }

        [JsonProperty("redirect")]
        public string RedirectUrl { get; set; }

        [JsonProperty("reference")]
        public string Reference { get; set; }

        #endregion
    }
}