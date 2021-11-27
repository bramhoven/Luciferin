using System;
using Newtonsoft.Json;

namespace FireflyWebImporter.BusinessLayer.Nordigen.Models
{
    public class Account
    {
        #region Properties

        public DateTime Created { get; set; }

        public string Iban { get; set; }

        public string Id { get; set; }

        public string InstitutionId { get; set; }

        public DateTime LastAccessed { get; set; }

        public string Status { get; set; }

        #endregion
    }
}