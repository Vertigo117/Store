using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;
using System;

namespace Store.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(product => product.Id);
            builder.Property<Guid?>("CategoryId");
            builder.HasMany(product => product.ProductOrders).WithOne(productOrder => productOrder.Product)
                .HasForeignKey(productOrder => productOrder.ProductId);
            builder.HasOne(product => product.Category).WithMany(category => category.Products)
                .HasForeignKey("CategoryId");
        }
    }
}
