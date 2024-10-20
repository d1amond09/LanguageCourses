using Contracts;
using LanguageCourses.Domain.Exceptions;
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
		var jobTitles = await _service.JobTitleService.GetAllJobTitlesAsync(trackChanges: false);
		return Ok(jobTitles);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetJobTitle(Guid id)
	{
		var jobTitle = _service.JobTitleService.GetJobTitleAsync(id, trackChanges: false)
			?? throw new JobTitleNotFoundException(id);
		return Ok(jobTitle);
	}

}
