using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolyBackend.DAL.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Title).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Description).IsRequired().HasMaxLength(2000);

            builder.Property(o => o.Status).HasConversion<string>().IsRequired();
            builder.Property(o => o.Type).HasConversion<string>().IsRequired();

            builder.HasOne(o => o.Client)
                   .WithMany() 
                   .HasForeignKey(o => o.ClientId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Messages)
                   .WithOne(m => m.Order)
                   .HasForeignKey(m => m.OrderId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}