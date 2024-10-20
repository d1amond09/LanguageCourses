using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/employees")]
[ApiController]
public class EmployeeController(IServiceManager service) : ControllerBase
{
	private readonly IServiceManager _service = service;

	[HttpGet]
	public async Task<IActionResult> GetEmployees()
	{
		var employees = await _service.EmployeeService.GetAllEmployeesAsync(trackChanges: false);
		return Ok(employees);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetEmployee(Guid id)
	{
		var employee = _service.EmployeeService.GetEmployeeAsync(id, trackChanges: false)
			?? throw new EmployeeNotFoundException(id);
		return Ok(employee);
	}

}
