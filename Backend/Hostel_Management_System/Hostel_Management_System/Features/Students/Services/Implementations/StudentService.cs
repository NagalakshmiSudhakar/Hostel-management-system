
using Hostel_Management_System.Features.Students.DTOs;
using Hostel_Management_System.Features.Students.Repositories.Interfaces;
using Hostel_Management_System.Features.Students.Services.Interfaces;
using Hostel_Management_System.Models;

namespace Hostel_Management_System.Features.Students.Services.Implementations;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _studentRepository;

    public StudentService(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task<List<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _studentRepository.GetAllStudentsAsync();

        return students.Select(student => new StudentDto
        {
            StudentId = student.StudentId,
            RegisterNumber = student.RegisterNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            DepartmentName = student.DepartmentName,
            IsActive = student.IsActive
        }).ToList();
    }

    public async Task<StudentDto> CreateStudentAsync(CreateStudentRequest request)
    {
        var student = new Student
        {
            RegisterNumber = request.RegisterNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DepartmentName = request.DepartmentName,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };

        await _studentRepository.AddStudentAsync(student);

        return new StudentDto
        {
            StudentId = student.StudentId,
            RegisterNumber = student.RegisterNumber,
            FirstName = student.FirstName,
            LastName = student.LastName,
            Email = student.Email,
            DepartmentName = student.DepartmentName,
            IsActive = student.IsActive
        };
    }

}
