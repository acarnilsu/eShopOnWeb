using MediatR;

namespace Microsoft.eShopWeb.Web.Features.UpdateUserStatus;

public class UpdateOrderStatus:IRequest
{
    public UpdateOrderStatus(int id, int status)
    {
        Id = id;
        StatusId = status;
    }

    public int Id { get; set; }
    public int StatusId { get; set;}
}
