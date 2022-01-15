using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;

namespace Store.Data.Configurations
{
    internal class ProductOrderConfiguration : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(productOrder => new { productOrder.OrderId, productOrder.ProductId });
            builder.HasOne(productOrder => productOrder.Order).WithMany(order => order.ProductOrders)
                .HasForeignKey(productOrder => productOrder.OrderId);
            builder.HasOne(productOrder => productOrder.Product).WithMany(product => product.ProductOrders)
                .HasForeignKey(productOrder => productOrder.OrderId);
        }
    }
}
