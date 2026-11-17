
using Hostel_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Hostel_Management_System.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");

        // Primary Key
        builder.HasKey(s => s.StudentId);

        // Register Number
        builder.Property(s => s.RegisterNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.HasIndex(s => s.RegisterNumber)
            .IsUnique();

        // First Name
        builder.Property(s => s.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        // Last Name
        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(50);

        // Email
        builder.Property(s => s.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(s => s.Email)
            .IsUnique();

        // Department
        builder.Property(s => s.DepartmentName)
            .IsRequired()
            .HasMaxLength(100);

        // Active Status
        builder.Property(s => s.IsActive)
            .HasDefaultValue(true);

        // Created Date
        builder.Property(s => s.CreatedAt)
            .IsRequired();
    }
}
