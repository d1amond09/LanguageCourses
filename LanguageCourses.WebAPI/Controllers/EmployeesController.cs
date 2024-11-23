using System.Text.Json;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Exceptions;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.WebAPI.ActionFilters;
using LanguageCourses.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[Route("api/employees")]
[ApiController]
public class EmployeesController(ISender sender) : ApiControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet(Name = "GetEmployees")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetEmployees([FromQuery] EmployeeParameters employeeParameters)
    {
        var linkParams = new LinkEmployeeParameters(employeeParameters, HttpContext);
        var baseResult = await _sender.Send(new GetEmployeesQuery(linkParams, TrackChanges: false));
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var (linkResponse, metaData) = baseResult.GetResult<(LinkResponse, MetaData)>();

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

        return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) :
            Ok(linkResponse.ShapedEntities);
    }

    [HttpGet("{id:guid}", Name = "GetEmployee")]
    public async Task<IActionResult> GetEmployee(Guid id)
    {
        var baseResult = await _sender.Send(new GetEmployeeQuery(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var products = baseResult.GetResult<EmployeeDto>();
        return Ok(products);
    }

    [HttpPost(Name = "CreateEmployee")]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeForCreationDto employee)
    {
        var baseResult = await _sender.Send(new CreateEmployeeCommand(employee));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var createdProduct = baseResult.GetResult<EmployeeDto>();

        return CreatedAtRoute("GetEmployee", new { id = createdProduct.Id }, createdProduct);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var baseResult = await _sender.Send(new DeleteEmployeeCommand(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeForUpdateDto employee)
    {
        var baseResult = await _sender.Send(new UpdateEmployeeCommand(id, employee, TrackChanges: true));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

}
