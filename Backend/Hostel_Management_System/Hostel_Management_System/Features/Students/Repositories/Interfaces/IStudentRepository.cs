
using Hostel_Management_System.Models;

namespace Hostel_Management_System.Features.Students.Repositories.Interfaces;

public interface IStudentRepository
{
    Task<List<Student>> GetAllStudentsAsync();

    Task AddStudentAsync(Student student);

    Task<Student?> GetStudentByIdAsync(int studentId);

    Task UpdateStudentAsync(Student student);

}
