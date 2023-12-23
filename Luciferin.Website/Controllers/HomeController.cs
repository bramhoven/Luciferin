namespace Luciferin.Website.Controllers;

using System.Threading.Tasks;
using Application.UseCases.Requisitions.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Home;

[Route("/")]
public class HomeController : Controller
{
    private readonly IMediator _mediator;

    public HomeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public virtual async Task<ActionResult> Index()
    {
        ViewData["Title"] = "Home";

        var getProviderAccountCommand = new GetImportAccountsCommand();
        var accounts = await _mediator.Send(getProviderAccountCommand);
        var model = new HomeIndexPageModel { ConfigurationStartUrl = "/configuration", ImportStartUrl = "/import", RequisitionList = new RequisitionList(accounts) };
        return View(model);
    }
}