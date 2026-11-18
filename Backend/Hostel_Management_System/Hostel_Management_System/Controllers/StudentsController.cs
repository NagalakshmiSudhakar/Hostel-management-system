
using Hostel_Management_System.Features.Students.DTOs;
using Hostel_Management_System.Features.Students.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Hostel_Management_System.Controllers;

[ApiController]
[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // GET: api/students
    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();

        return Ok(students);
    }

    // GET: api/students/1
    [HttpGet("{studentId:int}")]
    public async Task<IActionResult> GetStudentById(int studentId)
    {
        var student = await _studentService.GetStudentByIdAsync(studentId);

        if (student is null)
        {
            return NotFound(new
            {
                message = $"Student with ID {studentId} was not found."
            });
        }

        return Ok(student);
    }

    // POST: api/students
    [HttpPost]
    public async Task<IActionResult> CreateStudent(
        [FromBody] CreateStudentRequest request)
    {
        var student = await _studentService.CreateStudentAsync(request);

        return StatusCode(StatusCodes.Status201Created, student);
    }

    // PUT: api/students/1
    [HttpPut("{studentId:int}")]
    public async Task<IActionResult> UpdateStudent(
        int studentId,
        [FromBody] UpdateStudentRequest request)
    {
        var student = await _studentService.UpdateStudentAsync(
            studentId, request);

        if (student is null)
        {
            return NotFound(new
            {
                message = $"Student with ID {studentId} was not found."
            });
        }

        return Ok(student);
    }

    // DELETE: api/students/1
    [HttpDelete("{studentId:int}")]
    public async Task<IActionResult> DeactivateStudent(int studentId)
    {
        var result = await _studentService.DeactivateStudentAsync(studentId);

        if (!result)
        {
            return NotFound(new
            {
                message = $"Student with ID {studentId} was not found."
            });
        }

        return Ok(new
        {
            message = "Student deactivated successfully."
        });
    }
}
