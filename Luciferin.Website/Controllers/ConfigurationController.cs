using System;
using System.Threading.Tasks;
using Luciferin.BusinessLayer.Import;
using Luciferin.BusinessLayer.Nordigen;
using Luciferin.Website.Models;
using Luciferin.Website.Models.Configuration;
using Luciferin.Website.Models.Configuration.FormModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Luciferin.Website.Controllers;

using System.Collections.Generic;
using Application.UseCases.Requisitions.Add;
using Application.UseCases.Requisitions.Delete;
using Application.UseCases.Requisitions.Get;
using Core.Entities;

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
    [Route("/requisition")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> AddRequisition(ConfigurationIndexPageModel pageModel)
    {
        ViewData["Title"] = "Configuration";

        var formModel = pageModel.AddAccountFormModel;
        if (!ModelState.IsValid)
        {
            var model = await GetDefaultModel();
            model.AddAccountFormModel.InstitutionId = formModel.InstitutionId;
            model.AddAccountFormModel.RequisitionName = formModel.RequisitionName;
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

        var requestConnectionAccountCommand = new AddRequisitionCommand(formModel.RequisitionName, formModel.InstitutionId, returnUrl);
        var redirectUrl = await _mediator.Send(requestConnectionAccountCommand);

        return Redirect(redirectUrl);
    }

    [HttpPost]
    [Route("/requisition/delete")]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> DeleteRequisition([Bind(Prefix = "AccountList.accountId")] string requisitionId)
    {
        ViewData["Title"] = "Configuration";

        if (string.IsNullOrWhiteSpace(requisitionId))
            return RedirectToAction("Index", "Configuration");

        var deleteAccountCommand = new DeleteRequisitionCommand(requisitionId);
        await _mediator.Publish(deleteAccountCommand);


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
        var getProviderAccountsCommand = new GetImportAccountsCommand();
        var accounts = await _mediator.Send(getProviderAccountsCommand);
        var model = new ConfigurationIndexPageModel
        {
            AddAccountFormModel = new ConfigurationAddAccountFormModel
            {
                Institutions = await _nordigenManager.GetInstitutions("NL")
            },
            AccountList = new RequisitionList(accounts)
            {
                Deletable = true
            }
        };
        return model;
    }
}