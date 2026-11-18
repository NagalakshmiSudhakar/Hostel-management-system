
using Hostel_Management_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Hostel_Management_System.Data.Seeders;

public static class IdentitySeeder
{
    public static async Task SeedAsync(
        IServiceProvider serviceProvider,
        IConfiguration configuration)
    {
        var roleManager =
            serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var userManager =
            serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Read Admin credentials from User Secrets
        var adminEmail = configuration["Admin:Email"]
            ?? throw new InvalidOperationException("Admin email is missing.");

        var adminPassword = configuration["Admin:Password"]
            ?? throw new InvalidOperationException("Admin password is missing.");

        // Create roles if they do not exist
        string[] roles = { "Admin", "Warden" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                var roleResult = await roleManager.CreateAsync(
                    new IdentityRole(role));

                if (!roleResult.Succeeded)
                {
                    throw new InvalidOperationException(
                        string.Join(", ", roleResult.Errors.Select(e => e.Description)));
                }
            }
        }

        // Find existing Admin
        var admin = await userManager.FindByEmailAsync(adminEmail);

        // Create Admin if it does not exist
        if (admin == null)
        {
            admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Hostel Administrator",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(
                admin,
                adminPassword);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        // Ensure Admin role is assigned
        if (!await userManager.IsInRoleAsync(admin, "Admin"))
        {
            var roleResult = await userManager.AddToRoleAsync(admin, "Admin");

            if (!roleResult.Succeeded)
            {
                throw new InvalidOperationException(
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }
        }

        Console.WriteLine("Admin account and roles initialized successfully.");
    }
}
