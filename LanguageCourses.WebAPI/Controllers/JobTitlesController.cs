using Contracts;
using LanguageCourses.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/jobtitles")]
[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[ApiController]
public class JobTitlesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetJobTitles()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetJobTitles(Guid id)
    {
        throw new NotImplementedException();
    }

}
