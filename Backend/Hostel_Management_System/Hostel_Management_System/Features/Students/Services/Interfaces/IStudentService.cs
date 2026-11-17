
using Hostel_Management_System.Features.Students.DTOs;
using Hostel_Management_System.Models;

namespace Hostel_Management_System.Features.Students.Services.Interfaces;

public interface IStudentService
{
    Task<List<StudentDto>> GetAllStudentsAsync();

    Task<StudentDto> CreateStudentAsync(CreateStudentRequest request);
    Task<StudentDto?> UpdateStudentAsync(int studentId, UpdateStudentRequest request);
}
