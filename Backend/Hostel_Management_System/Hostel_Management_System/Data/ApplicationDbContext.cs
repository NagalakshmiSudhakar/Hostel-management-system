
using Hostel_Management_System.Data.Configurations;
using Hostel_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hostel_Management_System.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Student> Students { get; set; }
    public DbSet<Room> Rooms { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly);
        modelBuilder.ApplyConfiguration(new RoomConfiguration());
    }
}
