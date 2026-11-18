
using Hostel_Management_System.Features.Students.DTOs;

namespace Hostel_Management_System.Features.Students.Services.Interfaces;

public interface IStudentService
{
    // Get all students
    Task<List<StudentDto>> GetAllStudentsAsync();

    // Get student by ID
    Task<StudentDto?> GetStudentByIdAsync(int studentId);

    // Create student
    Task<StudentDto> CreateStudentAsync(CreateStudentRequest request);

    // Update student
    Task<StudentDto?> UpdateStudentAsync(
        int studentId,
        UpdateStudentRequest request);
}
