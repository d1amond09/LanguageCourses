using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
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
		var Courses = await _service.CourseService.GetAllCoursesAsync(trackChanges: false);
		return Ok(Courses);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetCompany(Guid id)
	{
		var company = _service.CourseService.GetCourseAsync(id, trackChanges: false);
		return Ok(company);
	}

}
