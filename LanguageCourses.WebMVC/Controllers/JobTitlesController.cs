using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebMVC.Controllers;

public class JobTitlesController(IServiceManager service) : Controller
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var jobTitles = await _service.JobTitleService.GetAllJobTitlesAsync(trackChanges: false);
        return View(jobTitles);
    }

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var jobTitle = _service.JobTitleService.GetJobTitleAsync(id, trackChanges: false)
            ?? throw new JobTitleNotFoundException(id);
        return View(jobTitle);
    }

}
