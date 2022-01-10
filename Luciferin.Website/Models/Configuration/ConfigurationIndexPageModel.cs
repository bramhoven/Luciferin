using System.Linq;
using Luciferin.Website.Models.Configuration.FormModels;

namespace Luciferin.Website.Models.Configuration
{
    public class ConfigurationIndexPageModel : PageModelBase
    {
        #region Properties

        public ConfigurationAddBankFormModel AddBankFormModel { get; set; }

        public RequisitionList RequisitionList { get; set; }

        #endregion
    }
}