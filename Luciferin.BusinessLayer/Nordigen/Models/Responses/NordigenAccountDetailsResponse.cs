using Newtonsoft.Json;

namespace Luciferin.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountDetailsResponse
    {
        #region Properties

        [JsonProperty("account")]
        public NordigenAccountDetails Account { get; set; }

        #endregion
    }
}