﻿using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Web.ViewModels;

public class OrderViewModel
{
    //private const string DEFAULT_STATUS = "Pending";

    public int OrderNumber { get; set; }
    public DateTimeOffset OrderDate { get; set; }
    public decimal Total { get; set; }
    //public string Status => DEFAULT_STATUS;
    public int Status { get; set; }
    public string? StatusName { get; set; }
    public Address? ShippingAddress { get; set; }
}
