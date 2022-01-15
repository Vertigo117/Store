using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;
using System;

namespace Store.Data.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(order => order.Id);
            builder.Property<Guid>("UserId");
            builder.HasOne(order => order.User).WithMany(user => user.Orders).HasForeignKey("UserId");
            builder.HasMany(order => order.ProductOrders).WithOne(productOrder => productOrder.Order)
                .HasForeignKey(productOrder => productOrder.OrderId);
        }
    }
}
