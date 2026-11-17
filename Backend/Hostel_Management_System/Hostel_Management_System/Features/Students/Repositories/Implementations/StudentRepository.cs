
using Hostel_Management_System.Data;
using Hostel_Management_System.Features.Students.Repositories.Interfaces;
using Hostel_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Hostel_Management_System.Features.Students.Repositories.Implementations;

public class StudentRepository : IStudentRepository
{
    private readonly ApplicationDbContext _context;

    public StudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Student>> GetAllStudentsAsync()
    {
        return await _context.Students
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddStudentAsync(Student student)
    {
        await _context.Students.AddAsync(student);

        await _context.SaveChangesAsync();
    }

    public async Task<Student?> GetStudentByIdAsync(int studentId)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.StudentId == studentId);
    }

    public async Task UpdateStudentAsync(Student student)
    {
        _context.Students.Update(student);

        await _context.SaveChangesAsync();
    }


}
