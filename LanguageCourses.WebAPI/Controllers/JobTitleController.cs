using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/jobtitles")]
[ApiController]
public class JobTitleController(IServiceManager service) : ControllerBase
{
	private readonly IServiceManager _service = service;

	[HttpGet]
	public async Task<IActionResult> GetJobTitles()
	{
		var JobTitles = await _service.JobTitleService.GetAllJobTitlesAsync(trackChanges: false);
		return Ok(JobTitles);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetCompany(Guid id)
	{
		var company = _service.JobTitleService.GetJobTitleAsync(id, trackChanges: false);
		return Ok(company);
	}

}
