using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Web.ViewModels;

public class UserOrderViewModel
{
    //private const string DEFAULT_STATUS = "Pending";
    public int Id { get; set; }
    public string? Name { get; set; }
    public int OrderNumber { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    //public string Status => DEFAULT_STATUS;
    public int StatusId {  get; set; }
    public string? StatusName {  get; set; }
    public List<OrderStatus> OrderStatuses { get; set; } = [];
    public Address? ShippingAddress { get; set; }
}
