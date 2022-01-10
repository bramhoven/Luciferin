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
    public partial class ConfigurationController : Controller
    {
        #region Fields

        private readonly IImportManager _importManager;

        private readonly INordigenManager _nordigenManager;

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
        public virtual async Task<ActionResult> AddBank(ConfigurationAddBankFormModel formModel)
        {
            if (string.IsNullOrWhiteSpace(formModel.InstitutionId))
                return RedirectToAction(MVC.Configuration.ActionNames.Index, MVC.Configuration.Name);

            var redirectUrl = new UriBuilder(Request.Host.Host)
            {
                Scheme = Uri.UriSchemeHttps,
                Path = Url.Action(MVC.Configuration.ActionNames.Index, MVC.Configuration.Name),
                Port = Request.Host.Port ?? 80
            }.ToString();
            var requisition = await _importManager.AddNewBank(formModel.InstitutionId, formModel.BankName, redirectUrl);

            return Redirect(requisition.Link);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual async Task<ActionResult> DeleteBank(string requisitionId)
        {
            if (string.IsNullOrWhiteSpace(requisitionId))
                return RedirectToAction(MVC.Configuration.ActionNames.Index, MVC.Configuration.Name);

            await _importManager.DeleteBank(requisitionId);

            return RedirectToAction(MVC.Configuration.ActionNames.Index, MVC.Configuration.Name);
        }

        public virtual async Task<ActionResult> Index()
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