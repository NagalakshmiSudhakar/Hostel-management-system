
using Hostel_Management_System.Features.Students.DTOs;

namespace Hostel_Management_System.Features.Students.Services.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();

    Task<StudentDto> CreateStudentAsync(CreateStudentRequest request);
}
