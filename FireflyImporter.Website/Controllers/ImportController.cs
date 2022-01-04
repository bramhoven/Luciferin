using System.Linq;
using System.Threading.Tasks;
using FireflyImporter.BusinessLayer.Import;
using FireflyImporter.Website.Classes.Queue;
using FireflyImporter.Website.Models;
using FireflyImporter.Website.Models.Import;
using Microsoft.AspNetCore.Mvc;

namespace FireflyImporter.Website.Controllers
{
    public partial class ImportController : Controller
    {
        #region Fields

        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

        private readonly IImportManager _importManager;

        #endregion

        #region Constructors

        public ImportController(IImportManager importManager, IBackgroundTaskQueue backgroundTaskQueue)
        {
            _importManager = importManager;
            _backgroundTaskQueue = backgroundTaskQueue;
        }

        #endregion

        #region Methods

        public virtual async Task<ActionResult> Index()
        {
            var requisitions = await _importManager.GetRequisitions();

            var model = new ImportIndexPageModel
            {
                ConfigurationStartUrl = Url.Action(MVC.Configuration.ActionNames.Index, MVC.Configuration.Name),
                RequisitionList = new RequisitionList(requisitions)
            };
            return View(model);
        }

        [HttpPost]
        public virtual async Task<ActionResult> StartImport()
        {
            await _backgroundTaskQueue.QueueBackgroundWorkItemAsync(_importManager.StartImport);

            return View();
        }

        #endregion
    }
}