using System.Linq;
using FireflyImporter.Models.Configuration.FormModels;

namespace FireflyImporter.Models.Configuration
{
    public class ConfigurationIndexPageModel
    {
        #region Properties

        public ConfigurationAddBankFormModel AddBankFormModel { get; set; }

        public RequisitionList RequisitionList { get; set; }

        #endregion
    }
}