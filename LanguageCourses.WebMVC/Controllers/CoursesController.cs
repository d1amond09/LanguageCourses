using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebMVC.Controllers;

public class CoursesController(IServiceManager service) : Controller
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    [ResponseCache(Duration = 248, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var courses = await _service.CourseService.GetAllCoursesAsync(trackChanges: false);
        return View(courses);
    }

    [HttpGet]
    [ResponseCache(Duration = 248, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var course = await _service.CourseService.GetCourseAsync(id, trackChanges: false)
            ?? throw new CourseNotFoundException(id);

        return View(course);
    }

}
