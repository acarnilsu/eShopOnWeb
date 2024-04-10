using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;

namespace Microsoft.eShopWeb.Infrastructure.Data.Config;
public class OrderStatusConfiguration : IEntityTypeConfiguration<OrderStatus>
{
    public void Configure(EntityTypeBuilder<OrderStatus> builder)
    {
        builder.Property(x => x.Id).IsRequired(true);
        builder.HasKey(ci => ci.Id);
        builder.HasData(new { Id = 1, Name = "Pending" });
        builder.HasData(new { Id = 2, Name = "Getting ready" });
        builder.HasData(new { Id = 3, Name = "Be canceled" });
        builder.HasData(new { Id = 4, Name = "Sent" });
    }
}
