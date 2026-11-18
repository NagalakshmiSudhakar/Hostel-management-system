namespace Hostel_Management_System.Features.Authentication.DTOs;

public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}