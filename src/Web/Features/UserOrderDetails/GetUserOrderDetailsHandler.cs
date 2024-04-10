using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.Features.OrderDetails;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.UserOrderDetails;

public class GetUserOrderDetailsHandler : IRequestHandler<GetUserOrderDetails, UserOrderDetailViewModel?>
{
    private readonly IReadRepository<Order> _orderRepository;
    private readonly IReadRepository<OrderStatus> _orderStatusRepository;

    public GetUserOrderDetailsHandler(IReadRepository<Order> orderRepository, IReadRepository<OrderStatus> orderStatusRepository)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
    }

    public async Task<UserOrderDetailViewModel?> Handle(GetUserOrderDetails request, CancellationToken cancellationToken)
    {
        var spec = new OrderWithItemsByIdSpec(request.OrderId);
        var order = await _orderRepository.FirstOrDefaultAsync(spec, cancellationToken);
        
        if (order == null)
        {
            return null;
        }
        var orderStatus = await _orderStatusRepository.GetByIdAsync(order.Status,cancellationToken);
        return new UserOrderDetailViewModel
        {
            Name = order.BuyerId,
            StatusId = order.Status,
            StatusName =orderStatus?.Name,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel
            {
                PictureUrl = oi.ItemOrdered.PictureUri,
                ProductId = oi.ItemOrdered.CatalogItemId,
                ProductName = oi.ItemOrdered.ProductName,
                UnitPrice = oi.UnitPrice,
                Units = oi.Units
            }).ToList(),
            OrderNumber = order.Id,
            ShippingAddress = order.ShipToAddress,
            Total = order.Total()
        };
    }
}
