using Newtonsoft.Json;

namespace FireflyImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenAccountDetailsResponse
    {
        #region Properties

        [JsonProperty("account")]
        public NordigenAccountDetails Account { get; set; }

        #endregion
    }
}