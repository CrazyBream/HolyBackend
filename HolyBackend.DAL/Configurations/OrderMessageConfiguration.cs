using HolyBackend.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HolyBackend.DAL.Configurations
{
    public class OrderMessageConfiguration : IEntityTypeConfiguration<OrderMessage>
    {
        public void Configure(EntityTypeBuilder<OrderMessage> builder)
        {
            builder.ToTable("OrderMessages");
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Text).IsRequired().HasMaxLength(1000);

            builder.HasOne(m => m.Sender)
                   .WithMany()
                   .HasForeignKey(m => m.SenderId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
