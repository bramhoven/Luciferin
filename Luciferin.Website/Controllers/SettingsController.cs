namespace Luciferin.Website.Controllers;

using System;
using System.Threading.Tasks;
using Application.UseCases.Settings.Get;
using Application.UseCases.Settings.Set;
using Classes.Settings;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models.Settings;

[Route("settings")]
public class SettingsController : Controller
{
    private readonly IMediator _mediator;

    public SettingsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public virtual async Task<ActionResult> Index()
    {
        ViewData["Title"] = "Settings";

        var model = await SetDefaultModel();
        return View(model);
    }

    [HttpPost]
    public virtual async Task<ActionResult> Index(SettingsPageModel model)
    {
        ViewData["Title"] = "Settings";

        var setAllSettingsCommand = new SetAllSettingsCommand(model.PlatformSettings.Settings);
        try
        {
            await _mediator.Send(setAllSettingsCommand);
            model.SuccessfullySaved = true;
        }
        catch (Exception)
        {
            model.SuccessfullySaved = false;
        }

        model = await SetDefaultModel(model);
        return View(model);
    }

    private async Task<SettingsPageModel> SetDefaultModel(SettingsPageModel model = null)
    {
        if (model == null)
        {
            model = new SettingsPageModel();
        }

        var getSettingsCommand = new GetSettingsCommand();
        var settings = await _mediator.Send(getSettingsCommand);

        model.PlatformSettings = PlatformSettingsMapper.MapSettingsCollectionToPlatformSettings(settings);
        return model;
    }
}