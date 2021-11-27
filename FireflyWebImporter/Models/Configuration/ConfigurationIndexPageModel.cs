using System.Linq;
using FireflyWebImporter.Models.Configuration.FormModels;

namespace FireflyWebImporter.Models.Configuration
{
    public class ConfigurationIndexPageModel
    {
        #region Properties

        public ConfigurationAddBankFormModel AddBankFormModel { get; set; }

        public RequisitionList RequisitionList { get; set; }

        #endregion
    }
}