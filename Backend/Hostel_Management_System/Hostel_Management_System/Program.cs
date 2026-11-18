using Microsoft.EntityFrameworkCore;
using Hostel_Management_System.Data;
using Hostel_Management_System.Features.Students.Repositories.Implementations;
using Hostel_Management_System.Features.Students.Repositories.Interfaces;
using Hostel_Management_System.Features.Students.Services.Implementations;
using Hostel_Management_System.Features.Students.Services.Interfaces;
using Hostel_Management_System.Features.Rooms.Repositories.Interfaces;
using Hostel_Management_System.Features.Rooms.Repositories.Implementations;
using Hostel_Management_System.Features.Rooms.Services.Interfaces;
using Hostel_Management_System.Features.Rooms.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(
    $"Connection string loaded: {!string.IsNullOrWhiteSpace(connectionString)}");

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IStudentService, StudentService>();
// Room Dependencies
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AngularPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();