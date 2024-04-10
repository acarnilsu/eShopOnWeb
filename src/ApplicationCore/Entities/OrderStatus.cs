using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.eShopWeb.ApplicationCore.Interfaces;

namespace Microsoft.eShopWeb.ApplicationCore.Entities;
public class OrderStatus : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
}
