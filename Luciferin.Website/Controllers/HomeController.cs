using System.Threading.Tasks;
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

        private readonly INordigenManager _nordigenManager;

        #endregion

        #region Constructors

        public HomeController(INordigenManager nordigenManager)
        {
            _nordigenManager = nordigenManager;
        }

        #endregion

        #region Methods

        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            var model = new HomeIndexPageModel
            {
                ConfigurationStartUrl = Url.Action(MVC.Configuration.Index()),
                ImportStartUrl = Url.Action(MVC.Import.Index()),
                RequisitionList = new RequisitionList(await _nordigenManager.GetRequisitions())
            };
            return View(model);
        }

        #endregion
    }
}