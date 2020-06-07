using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Bring value type ProductItemOrdered into table
            builder.OwnsOne(i => i.ItemOrdered, io => {
                io.WithOwner();
                io.Property(io => io.ProductItemId).IsRequired();
                io.Property(io => io.ProductName).IsRequired().HasMaxLength(100);
                io.Property(io => io.PictureUrl).IsRequired().HasMaxLength(200);
            });

            builder.Property(i => i.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}