using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Luciferin.Website.Classes.Queue;
using Luciferin.Website.Models;
using Luciferin.Website.Models.Import;
using Microsoft.AspNetCore.Mvc;

namespace Luciferin.Website.Controllers
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
        public virtual async Task<ActionResult> Start()
        {
            await _backgroundTaskQueue.QueueBackgroundWorkItemAsync(_importManager.StartImport);
            return MVC.Import.RedirectToAction(MVC.Import.ActionNames.Status);
        }

        [HttpGet]
        public async virtual Task<ActionResult> Status()
        {
            var model = new PageModelBase { FullWidth = true };
            return View(model);
        }

        #endregion
    }
}