using System.Linq;
using FireflyImporter.Website.Models.Configuration.FormModels;

namespace FireflyImporter.Website.Models.Configuration
{
    public class ConfigurationIndexPageModel
    {
        #region Properties

        public ConfigurationAddBankFormModel AddBankFormModel { get; set; }

        public RequisitionList RequisitionList { get; set; }

        #endregion
    }
}