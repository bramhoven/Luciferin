using System.Linq;

namespace FireflyWebImporter.Models.Home
{
    public class HomeIndexPageModel
    {
        #region Properties

        public string ConfigurationStartUrl { get; set; }
        
        public string ImportStartUrl { get; set; }

        public RequisitionList RequisitionList { get; set; }

        #endregion
    }
}