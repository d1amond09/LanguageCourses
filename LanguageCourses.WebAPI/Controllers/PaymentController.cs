using Contracts;
using LanguageCourses.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/payments")]
[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[ApiController]
public class PaymentsController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetPayments()
    {
        throw new NotImplementedException();
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetPayments(Guid id)
    {
        throw new NotImplementedException();
    }

}
