
using Hostel_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel_Management_System.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.HasKey(r => r.RoomId);

        builder.Property(r => r.RoomNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(r => r.RoomNumber)
            .IsUnique();

        builder.Property(r => r.FloorNumber)
            .IsRequired();

        builder.Property(r => r.RoomType)
            .IsRequired()
            .HasMaxLength(30);

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.Property(r => r.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(r => r.CreatedAt)
            .IsRequired();
    }
}
