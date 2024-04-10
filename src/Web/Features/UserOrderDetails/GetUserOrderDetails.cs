using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.UserOrderDetails;

public class GetUserOrderDetails:IRequest<UserOrderDetailViewModel>
{
    public GetUserOrderDetails( int orderId)
    {
        OrderId = orderId;
    }

    public int OrderId { get; set; }
}
