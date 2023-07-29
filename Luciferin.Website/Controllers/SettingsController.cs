using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Configuration;
using Luciferin.BusinessLayer.Settings;
using Luciferin.Website.Models.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Luciferin.Website.Controllers
{
    [Route("settings")]
    public partial class SettingsController : Controller
    {
        #region Fields

        private readonly ISettingsManager _settingsManager;

        private readonly LuciferinSettings _luciferinSettings;

        #endregion

        #region Constructors

        public SettingsController(ISettingsManager settingsManager, IOptions<LuciferinSettings> luciferinSettings)
        {
            _settingsManager = settingsManager;
            _luciferinSettings = luciferinSettings.Value;
        }

        #endregion

        #region Methods

        [HttpGet]
        public virtual ActionResult Index()
        {
            ViewData["Title"] = "Settings";
            
            var model = SetDefaultModel();
            return View(model);
        }

        [HttpPost]
        public async virtual Task<ActionResult> Index(SettingsPageModel model)
        {
            ViewData["Title"] = "Settings";
            
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