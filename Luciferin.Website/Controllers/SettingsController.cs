using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Settings;
using Luciferin.Website.Models.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Luciferin.Website.Controllers
{
    [Route("settings")]
    public partial class SettingsController : Controller
    {
        #region Fields

        private readonly ISettingsManager _settingsManager;

        #endregion

        #region Constructors

        public SettingsController(ISettingsManager settingsManager)
        {
            _settingsManager = settingsManager;
        }

        #endregion

        #region Methods

        [HttpGet]
        public virtual ActionResult Index()
        {
            var model = SetDefaultModel();
            return View(model);
        }

        [HttpPost]
        public async virtual Task<ActionResult> Index(SettingsPageModel model)
        {
            model.SuccessfullySaved = _settingsManager.UpdateSettings(model.Settings.Settings);

            model = SetDefaultModel(model);
            return View(model);
        }

        private SettingsPageModel SetDefaultModel(SettingsPageModel model = null)
        {
            if (model == null)
                model = new SettingsPageModel();

            model.Settings = _settingsManager.GetPlatformSettings();
            return model;
        }

        #endregion
    }
}