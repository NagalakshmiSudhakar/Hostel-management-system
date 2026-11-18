using Hostel_Management_System.Features.Authentication.DTOs;

namespace Hostel_Management_System.Features.Authentication.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse?> LoginAsync(LoginRequest request);
}