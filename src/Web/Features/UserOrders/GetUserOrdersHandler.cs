using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.UserOrders;

public class GetUserOrdersHandler:IRequestHandler<GetUserOrders,IEnumerable<UserOrderViewModel>>
{
    private readonly IReadRepository<Order> _orderRepository;
    private readonly IReadRepository<OrderStatus> _orderStatusRepository;

    public GetUserOrdersHandler(IReadRepository<Order> orderRepository, IReadRepository<OrderStatus> orderStatusRepository)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
    }

    public async Task<IEnumerable<UserOrderViewModel>> Handle(GetUserOrders request, CancellationToken cancellationToken)
    {
        var specification = new AllCustomerOrdersSpecification();
        var orders = await _orderRepository.ListAsync(specification, cancellationToken);
        var ordersStatuses = await _orderStatusRepository.ListAsync(cancellationToken);

        return orders.Select(o => new UserOrderViewModel
        {
            Id=o.Id,
            Name=o.BuyerId,
            StatusId=o.Status,
            StatusName=ordersStatuses.FirstOrDefault(x=>x.Id==o.Status)?.Name,
            OrderDate = o.OrderDate,
            OrderNumber = o.Id,
            ShippingAddress = o.ShipToAddress,
            Total = o.Total(),
            OrderStatuses=ordersStatuses
        });
    }
}
