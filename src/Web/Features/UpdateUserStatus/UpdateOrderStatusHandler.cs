using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using NuGet.Protocol.Plugins;

namespace Microsoft.eShopWeb.Web.Features.UpdateUserStatus;

public class UpdateOrderStatusHandler : IRequestHandler<UpdateOrderStatus>
{
    private readonly IOrderService _orderService;

    public UpdateOrderStatusHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task Handle(UpdateOrderStatus request, CancellationToken cancellationToken)
    {
        await _orderService.ChangeStatusOrderAsync(request.Id, request.StatusId);
    }
}
