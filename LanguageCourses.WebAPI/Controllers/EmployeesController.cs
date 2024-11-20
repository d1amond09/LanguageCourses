using Contracts;
using LanguageCourses.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[Route("api/employees")]
[ApiController]
public class EmployeesController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployee(Guid id)
    {
        throw new NotImplementedException();
    }

}
