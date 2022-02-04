using System.Linq;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Settings;
using Luciferin.Website.Models.Settings;
using Microsoft.AspNetCore.Mvc;

namespace Luciferin.Website.Controllers
{
    public class SettingsController : Controller
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

        public async Task<ActionResult> Index()
        {
            var model = new SettingsIndexPageModel
            {
                Settings = _settingsManager.GetPlatformSettings()
            };

            return View(model);
        }

        #endregion
    }
}