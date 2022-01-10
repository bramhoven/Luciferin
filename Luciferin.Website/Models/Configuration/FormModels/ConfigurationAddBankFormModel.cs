using System.Collections.Generic;
using Luciferin.BusinessLayer.Nordigen.Models;

namespace Luciferin.Website.Models.Configuration.FormModels
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