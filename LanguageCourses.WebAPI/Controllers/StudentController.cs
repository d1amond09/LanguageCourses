using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/students")]
[ApiController]
public class StudentController(IServiceManager service) : ControllerBase
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> GetStudents()
    {
        var students = await _service.StudentService.GetAllStudentsAsync(trackChanges: false);
        return Ok(students);
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetStudent(Guid id)
    {
        var student = _service.StudentService.GetStudentAsync(id, trackChanges: false)
                    ?? throw new StudentNotFoundException(id);
        return Ok(student);
    }

}
