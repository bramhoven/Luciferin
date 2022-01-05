using System.Collections.Generic;
using FireflyImporter.BusinessLayer.Nordigen.Models;

namespace FireflyImporter.Website.Models.Configuration.FormModels
{
    public class ConfigurationAddBankFormModel
    {
        #region Properties

        public string BankName { get; set; }

        public string InstitutionId { get; set; }

        public ICollection<Institution> Institutions { get; set; }

        #endregion
    }
}