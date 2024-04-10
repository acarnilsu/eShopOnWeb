namespace Microsoft.eShopWeb.Web.ViewModels;

public class UserOrderDetailViewModel:UserOrderViewModel
{
    public List<OrderItemViewModel> OrderItems { get; set; } = new();
}
