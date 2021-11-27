using System.Threading.Tasks;
using FireflyWebImporter.BusinessLayer.Nordigen;
using FireflyWebImporter.Models;
using FireflyWebImporter.Models.Home;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FireflyWebImporter.Controllers
{
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