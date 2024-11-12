using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebMVC.Controllers;

public class StudentsController(IServiceManager service) : Controller
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var students = await _service.StudentService.GetAllStudentsAsync(trackChanges: false);
        return View(students);
    }

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var student = _service.StudentService.GetStudentAsync(id, trackChanges: false)
                    ?? throw new StudentNotFoundException(id);
        return View(student);
    }

}
