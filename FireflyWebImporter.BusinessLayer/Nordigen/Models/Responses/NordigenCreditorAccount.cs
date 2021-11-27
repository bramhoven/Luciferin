using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models.Responses
{
    public class NordigenCreditorAccount
    {
        #region Properties

        [JsonProperty("iban")]
        public string Iban { get; set; }

        #endregion
    }
}