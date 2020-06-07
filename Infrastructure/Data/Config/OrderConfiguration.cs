using System;
using Core.Entities.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Bring ShipToAddress into Orders table
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {
                a.WithOwner();
                a.Property(a => a.FirstName).IsRequired().HasMaxLength(100);
                a.Property(a => a.LastName).IsRequired().HasMaxLength(100);
                a.Property(a => a.Street).IsRequired().HasMaxLength(100);
                a.Property(a => a.City).IsRequired().HasMaxLength(100);
                a.Property(a => a.Zipcode).IsRequired().HasMaxLength(50);
                a.Property(a => a.State).IsRequired().HasMaxLength(50);
            });

            // Converts the Status going in and out of the DB
            builder.Property(o => o.Status)
                    .HasConversion(
                        s => s.ToString(),
                        s => (OrderStatus) Enum.Parse(typeof(OrderStatus), s)
                    );

            builder.Property(o => o.BuyerEmail).IsRequired().HasMaxLength(100);

            builder.HasMany(o => o.OrderItems).WithOne().OnDelete(DeleteBehavior.Cascade);
        }
    }
}