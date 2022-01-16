using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities;
using System;

namespace Store.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);
            builder.Property<Guid?>("RoleId");
            builder.HasOne(user => user.Role).WithMany(role => role.Users)
                .HasForeignKey("RoleId");
            builder.HasMany(user => user.Orders).WithOne(order => order.User).HasForeignKey("UserId");
        }
    }
}
