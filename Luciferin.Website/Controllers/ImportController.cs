namespace Luciferin.Website.Controllers;

using System.Threading.Tasks;
using Application.UseCases.Requisitions.Get;
using BusinessLayer.Import;
using Classes.Queue;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Models.Import;

[Route("import")]
public class ImportController : Controller
{
    private readonly IBackgroundTaskQueue _backgroundTaskQueue;

    private readonly IMediator _mediator;

    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ImportController(IBackgroundTaskQueue backgroundTaskQueue, IServiceScopeFactory serviceScopeFactory, IMediator mediator)
    {
        _backgroundTaskQueue = backgroundTaskQueue;
        _serviceScopeFactory = serviceScopeFactory;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        ViewData["Title"] = "Import";

        var getProviderAccountsCommand = new GetImportAccountsCommand();
        var accounts = await _mediator.Send(getProviderAccountsCommand);
        var model = new ImportIndexPageModel { ConfigurationStartUrl = "/configuration", RequisitionList = new RequisitionList(accounts) };
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
}