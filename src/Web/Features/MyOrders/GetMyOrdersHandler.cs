using MediatR;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;
using Microsoft.eShopWeb.ApplicationCore.Specifications;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.MyOrders;

public class GetMyOrdersHandler : IRequestHandler<GetMyOrders, IEnumerable<OrderViewModel>>
{
    private readonly IReadRepository<Order> _orderRepository;
    private readonly IReadRepository<OrderStatus> _orderStatusRepository;

    public GetMyOrdersHandler(IReadRepository<Order> orderRepository, IReadRepository<OrderStatus> orderStatusRepository)
    {
        _orderRepository = orderRepository;
        _orderStatusRepository = orderStatusRepository;
    }

    public async Task<IEnumerable<OrderViewModel>> Handle(GetMyOrders request,
        CancellationToken cancellationToken)
    {
        var specification = new CustomerOrdersSpecification(request.UserName);
        var orders = await _orderRepository.ListAsync(specification, cancellationToken);
        var orderStatuses = await _orderStatusRepository.ListAsync(cancellationToken);
        return orders.Select(o => new OrderViewModel
        {
            Status=o.Status,
            StatusName=orderStatuses.FirstOrDefault(x=>x.Id==o.Status)?.Name,
            OrderDate = o.OrderDate,
            OrderNumber = o.Id,
            ShippingAddress = o.ShipToAddress,
            Total = o.Total()
        });
    }
}
