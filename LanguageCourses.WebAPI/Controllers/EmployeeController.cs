using Contracts;
using Contracts.Services;
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
		var Employees = await _service.EmployeeService.GetAllEmployeesAsync(trackChanges: false);
		return Ok(Employees);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetCompany(Guid id)
	{
		var company = _service.EmployeeService.GetEmployeeAsync(id, trackChanges: false);
		return Ok(company);
	}

}
