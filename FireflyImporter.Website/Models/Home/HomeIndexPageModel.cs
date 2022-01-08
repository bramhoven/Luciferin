using System.Linq;

namespace FireflyImporter.Website.Models.Home
{
    public class HomeIndexPageModel : PageModelBase
    {
        #region Properties

        public string ConfigurationStartUrl { get; set; }

        public string ImportStartUrl { get; set; }

        public RequisitionList RequisitionList { get; set; }

        #endregion
    }
}