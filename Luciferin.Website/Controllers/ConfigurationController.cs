using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.Website.Models;
using Luciferin.Website.Models.Configuration;
using Luciferin.Website.Models.Configuration.FormModels;
using Microsoft.AspNetCore.Mvc;

namespace Luciferin.Website.Controllers
{
    [Route("configuration")]
    public class ConfigurationController : Controller
    {
        #region Fields

        private readonly IImportManager _importManager;

        private readonly INordigenManager _nordigenManager;

        private const string ForwardedPortHeader = "X-Forwarded-Port";

        #endregion

        #region Constructors

        public ConfigurationController(INordigenManager nordigenManager, IImportManager importManager)
        {
            _nordigenManager = nordigenManager;
            _importManager = importManager;
        }

        #endregion

        #region Methods

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddBank(ConfigurationAddBankFormModel formModel)
        {
            if (string.IsNullOrWhiteSpace(formModel.InstitutionId))
                return RedirectToAction("Index", "Configuration");

            var forwardHeaderPort = Request.Headers.ContainsKey(ForwardedPortHeader) ? (int?)int.Parse(Request.Headers[ForwardedPortHeader]) : null;
            var redirectUrl = new UriBuilder(Request.Host.Host)
            {
                Scheme = Uri.UriSchemeHttps,
                Path = "/configuration",
                Port = forwardHeaderPort ?? Request.Host.Port ?? 80
            }.ToString();
            var requisition = await _importManager.AddNewBank(formModel.InstitutionId, formModel.BankName, redirectUrl);

            return Redirect(requisition.Link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBank(string requisitionId)
        {
            if (string.IsNullOrWhiteSpace(requisitionId))
                return RedirectToAction("Index", "Configuration");

            await _importManager.DeleteBank(requisitionId);

            return RedirectToAction("Index", "Configuration");
        }
        
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = new ConfigurationIndexPageModel
            {
                AddBankFormModel = new ConfigurationAddBankFormModel
                {
                    Institutions = await _nordigenManager.GetInstitutions("NL")
                },
                RequisitionList = new RequisitionList(await _nordigenManager.GetRequisitions())
                {
                    Deletable = true
                }
            };
            return View(model);
        }

        #endregion
    }
}