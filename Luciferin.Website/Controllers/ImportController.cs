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
    public partial class ImportController : Controller
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
            var scope = _serviceScopeFactory.CreateScope();
            var scopedImportManager = scope.ServiceProvider.GetService<IImportManager>();
            await _backgroundTaskQueue.QueueBackgroundWorkItemAsync(cancellationToken => scopedImportManager.StartImport(scope, cancellationToken));

            return MVC.Import.RedirectToAction(MVC.Import.ActionNames.Status);
        }

        [HttpGet("status")]
        public virtual ActionResult Status()
        {
            var model = new PageModelBase { FullWidth = true };
            return View(model);
        }

        #endregion
    }
}