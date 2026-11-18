
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

    // GET: Retrieve all students
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

    // GET: Retrieve student by ID
    public async Task<StudentDto?> GetStudentByIdAsync(int studentId)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);

        if (student is null)
        {
            return null;
        }

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

    // POST: Create a new student
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

    // PUT: Update an existing student
    public async Task<StudentDto?> UpdateStudentAsync(
        int studentId,
        UpdateStudentRequest request)
    {
        var student = await _studentRepository.GetStudentByIdAsync(studentId);

        if (student is null)
        {
            return null;
        }

        student.RegisterNumber = request.RegisterNumber;
        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Email = request.Email;
        student.DepartmentName = request.DepartmentName;
        student.IsActive = request.IsActive;

        await _studentRepository.UpdateStudentAsync(student);

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
