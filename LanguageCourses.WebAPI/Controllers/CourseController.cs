using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/courses")]
[ApiController]
public class CourseController(IServiceManager service) : ControllerBase
{
	private readonly IServiceManager _service = service;

	[HttpGet]
	public async Task<IActionResult> GetCourses()
	{
		var courses = await _service.CourseService.GetAllCoursesAsync(trackChanges: false);
		return Ok(courses);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetCourse(Guid id)
	{
		var course = _service.CourseService.GetCourseAsync(id, trackChanges: false)
			?? throw new CourseNotFoundException(id);

		return Ok(course);
	}

}
