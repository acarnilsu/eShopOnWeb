using Ardalis.GuardClauses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.eShopWeb.Web.Features.MyOrders;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Microsoft.eShopWeb.Web.Features.UpdateUserStatus;
using Microsoft.eShopWeb.Web.Features.UserOrderDetails;
using Microsoft.eShopWeb.Web.Features.UserOrders;

namespace Microsoft.eShopWeb.Web.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
[Authorize] // Controllers that mainly require Authorization still use Controller/View; other pages use Pages
[Route("[controller]/[action]")]
public class OrderController : Controller
{
    private readonly IMediator _mediator;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> MyOrders()
    {   
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        var viewModel = await _mediator.Send(new GetMyOrders(User.Identity.Name));

        return View(viewModel);
    }


    [HttpGet("{orderId}")]
    public async Task<IActionResult> Detail(int orderId)
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        var viewModel = await _mediator.Send(new GetOrderDetails(User.Identity.Name, orderId));

        if (viewModel == null)
        {
            return BadRequest("No such order found for this user.");
        }

        return View(viewModel);
    }



    [Authorize(Roles = "Administrators")]
    [HttpGet]
    public async Task<IActionResult> UserOrders()
    {
        Guard.Against.Null(User?.Identity?.Name, nameof(User.Identity.Name));
        var viewModel = await _mediator.Send(new GetUserOrders());

        return View(viewModel);
    }


    [Authorize(Roles = "Administrators")]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> UserOrderDetail(int orderId)
    {
        var viewModel = await _mediator.Send(new GetUserOrderDetails(orderId));

        if (viewModel == null)
        {
            return BadRequest("No such order found for this user.");
        }

        return View(viewModel);
    }


    [Authorize(Roles = "Administrators")]
    [HttpPost]
    public async Task<IActionResult> ChangeStatusUserOrders(int id, int statusId)
    {
        try
        {
            await _mediator.Send(new UpdateOrderStatus(id, statusId));
            return RedirectToAction(nameof(UserOrders));
        }
        catch (Exception)
        {
            return BadRequest($"StatusId is not found at StatusList.");
        }

    }
}
