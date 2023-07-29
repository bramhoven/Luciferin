using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Luciferin.Website.Classes.Queue;
using Luciferin.Website.Models;
using Luciferin.Website.Models.Import;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Luciferin.Website.Controllers
{
    [Route("import")]
    public class ImportController : Controller
    {
        #region Constructors

        public ImportController(IImportManager importManager, IBackgroundTaskQueue backgroundTaskQueue,
                                IServiceScopeFactory serviceScopeFactory)
        {
            _importManager = importManager;
            _backgroundTaskQueue = backgroundTaskQueue;
            _serviceScopeFactory = serviceScopeFactory;
        }

        #endregion

        #region Fields

        private readonly IBackgroundTaskQueue _backgroundTaskQueue;

        private readonly IImportManager _importManager;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        #endregion

        #region Methods

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            ViewData["Title"] = "Import";
            
            var requisitions = await _importManager.GetImportableRequisitions();
            var model = new ImportIndexPageModel
            {
                ConfigurationStartUrl = "/configuration",
                RequisitionList = new RequisitionList(requisitions)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Start()
        {
            ViewData["Title"] = "Import";
            
            var scope = _serviceScopeFactory.CreateScope();
            var scopedImportManager = scope.ServiceProvider.GetService<IImportManager>();
            await _backgroundTaskQueue.QueueBackgroundWorkItemAsync(cancellationToken => scopedImportManager.StartImport(scope, cancellationToken));

            return RedirectToAction("Status");
        }

        [HttpGet("status")]
        public ActionResult Status()
        {
            ViewData["Title"] = "Import status";
            
            var model = new PageModelBase { FullWidth = true };
            return View(model);
        }

        #endregion
    }
}