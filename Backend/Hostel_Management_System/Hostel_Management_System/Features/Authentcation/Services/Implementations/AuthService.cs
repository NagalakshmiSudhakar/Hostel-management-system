
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Hostel_Management_System.Features.Authentication.DTOs;
using Hostel_Management_System.Features.Authentication.Services.Interfaces;
using Hostel_Management_System.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Hostel_Management_System.Features.Authentication.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        // Find user by email
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            return null;
        }

        // Verify password using Identity
        var isPasswordValid =
            await _userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            return null;
        }

        // Get user's role
        var roles = await _userManager.GetRolesAsync(user);

        var role = roles.FirstOrDefault();

        if (string.IsNullOrWhiteSpace(role))
        {
            return null;
        }

        // Read JWT configuration
        var key = _configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT key is missing.");

        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];

        var expiryMinutes =
            _configuration.GetValue<int>("Jwt:ExpiryMinutes", 30);

        var expiresAt = DateTime.UtcNow.AddMinutes(expiryMinutes);

        // Create JWT claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        // Create signing credentials
        var signingKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(key));

        var credentials = new SigningCredentials(
            signingKey,
            SecurityAlgorithms.HmacSha256);

        // Generate JWT
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler()
            .WriteToken(token);

        return new AuthResponse
        {
            UserId = user.Id,
            FullName = user.FullName,
            Email = user.Email ?? string.Empty,
            Role = role,
            Token = tokenString,
            ExpiresAt = expiresAt
        };
    }
}
