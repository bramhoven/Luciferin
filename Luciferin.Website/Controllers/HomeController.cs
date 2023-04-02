using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.Website.Models;
using Luciferin.Website.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Luciferin.Website.Controllers
{
    [Route("/")]
    public partial class HomeController : Controller
    {
        #region Fields

        private readonly IImportManager _importManager;

        #endregion

        #region Constructors

        public HomeController(IImportManager importManager)
        {
            _importManager = importManager;
        }

        #endregion

        #region Methods

        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            var model = new HomeIndexPageModel
            {
                ConfigurationStartUrl = "/configuration",
                ImportStartUrl = "/import",
                RequisitionList = new RequisitionList(await _importManager.GetImportableRequisitions())
            };
            return View(model);
        }

        #endregion
    }
}