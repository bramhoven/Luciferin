namespace Luciferin.Website.Controllers;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.UseCases.Requisitions.Get;
using Core.Entities;
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

        ICollection<Requisition> requisitions;
        try
        {
            var getProviderAccountCommand = new GetRequisitionsCommand();
            requisitions = await _mediator.Send(getProviderAccountCommand);
        }
        catch (Exception)
        {
            requisitions = new List<Requisition>();
        }

        var model = new HomeIndexPageModel { ConfigurationStartUrl = "/configuration", ImportStartUrl = "/import", RequisitionList = new RequisitionList(requisitions) };
        return View(model);
    }
}