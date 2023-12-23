using System.Linq;
using Luciferin.Website.Models.Configuration.FormModels;

namespace Luciferin.Website.Models.Configuration
{
    public class ConfigurationIndexPageModel : PageModelBase
    {
        #region Properties

        public ConfigurationAddAccountFormModel AddAccountFormModel { get; set; }

        public RequisitionList? AccountList { get; set; }

        #endregion
    }
}