
namespace Hostel_Management_System.Features.Students.DTOs;

public class CreateStudentRequest
{
    public string RegisterNumber { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;
}
