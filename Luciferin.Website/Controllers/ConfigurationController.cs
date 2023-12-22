using System;
using System.Threading.Tasks;
using Luciferin.Application.UseCases.Accounts.RequestConnection;
using Luciferin.BusinessLayer.Import;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.Website.Models;
using Luciferin.Website.Models.Configuration;
using Luciferin.Website.Models.Configuration.FormModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Luciferin.Website.Controllers;

[Route("configuration")]
public class ConfigurationController : Controller
{
    public ConfigurationController(INordigenManager nordigenManager, IImportManager importManager, IMediator mediator)
    {
        _nordigenManager = nordigenManager;
        _importManager = importManager;
        _mediator = mediator;
    }

    private readonly IMediator _mediator;

    private readonly IImportManager _importManager;

    private readonly INordigenManager _nordigenManager;

    private const string ForwardedPortHeader = "X-Forwarded-Port";

    [HttpPost]
    [Route("/bank")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddBank(ConfigurationIndexPageModel pageModel)
    {
        ViewData["Title"] = "Configuration";

        var formModel = pageModel.AddBankFormModel;
        if (!ModelState.IsValid)
        {
            var model = await GetDefaultModel();
            model.AddBankFormModel.InstitutionId = formModel.InstitutionId;
            model.AddBankFormModel.BankName = formModel.BankName;
            return View(nameof(Index), model);
        }

        var forwardHeaderPort = Request.Headers.ContainsKey(ForwardedPortHeader)
            ? (int?)int.Parse(Request.Headers[ForwardedPortHeader])
            : null;
        var returnUrl = new UriBuilder(Request.Host.Host)
        {
            Scheme = Uri.UriSchemeHttps,
            Path = "/configuration",
            Port = forwardHeaderPort ?? Request.Host.Port ?? 80
        }.ToString();
        //var requisition = await _importManager.AddNewBank(formModel.InstitutionId, formModel.BankName, returnUrl);

        var requestConnectionAccountCommand = new RequestConnectionAccountCommand(formModel.BankName, formModel.InstitutionId, returnUrl);
        var redirectUrl = await _mediator.Send(requestConnectionAccountCommand);

        return Redirect(redirectUrl);
    }

    [HttpPost]
    [Route("/bank/delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteBank([Bind(Prefix = "RequisitionList.requisitionId")] string requisitionId)
    {
        ViewData["Title"] = "Configuration";

        if (string.IsNullOrWhiteSpace(requisitionId))
            return RedirectToAction("Index", "Configuration");

        await _importManager.DeleteBank(requisitionId);

        return RedirectToAction("Index", "Configuration");
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        ViewData["Title"] = "Configuration";

        return View(await GetDefaultModel());
    }

    private async Task<ConfigurationIndexPageModel> GetDefaultModel()
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
        return model;
    }
}