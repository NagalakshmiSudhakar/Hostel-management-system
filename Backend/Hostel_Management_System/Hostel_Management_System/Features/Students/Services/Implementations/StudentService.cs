
using Hostel_Management_System.Features.Students.DTOs;
using Hostel_Management_System.Features.Students.Repositories.Interfaces;
using Hostel_Management_System.Features.Students.Services.Interfaces;

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
}
