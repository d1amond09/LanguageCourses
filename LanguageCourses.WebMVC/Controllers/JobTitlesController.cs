using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebMVC.Controllers;

public class JobTitlesController(IServiceManager service) : Controller
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    [ResponseCache(Duration = 248, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Index()
    {
        var jobTitles = await _service.JobTitleService.GetAllJobTitlesAsync(trackChanges: false);
        return View(jobTitles);
    }

    [HttpGet]
    [ResponseCache(Duration = 248, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(Guid id)
    {
        var jobTitle = await _service.JobTitleService.GetJobTitleAsync(id, trackChanges: false)
            ?? throw new JobTitleNotFoundException(id);
        return View(jobTitle);
    }

}
