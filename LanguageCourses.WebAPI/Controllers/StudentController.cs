using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
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
}
