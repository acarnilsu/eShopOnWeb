using MediatR;
using Microsoft.eShopWeb.Web.ViewModels;

namespace Microsoft.eShopWeb.Web.Features.UserOrders;

public class GetUserOrders: IRequest<IEnumerable<UserOrderViewModel>>
{

}
