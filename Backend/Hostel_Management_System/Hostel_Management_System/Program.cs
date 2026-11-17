using Microsoft.EntityFrameworkCore;
using Hostel_Management_System.Data;
using Hostel_Management_System.Features.Students.Repositories.Implementations;
using Hostel_Management_System.Features.Students.Repositories.Interfaces;
using Hostel_Management_System.Features.Students.Services.Implementations;
using Hostel_Management_System.Features.Students.Services.Interfaces;

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