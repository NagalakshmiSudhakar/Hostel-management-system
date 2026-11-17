
using Hostel_Management_System.Features.Students.DTOs;
using Hostel_Management_System.Features.Students.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hostel_Management_System.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudentsAsync();

        return Ok(students);
    }


    [HttpPost]
    public async Task<IActionResult> CreateStudent(
        [FromBody] CreateStudentRequest request)
    {
        var student = await _studentService.CreateStudentAsync(request);

        return StatusCode(StatusCodes.Status201Created, student);
    }

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

}
