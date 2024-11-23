using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Exceptions;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.WebAPI.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LanguageCourses.WebAPI.Extensions;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/students")]
[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[ApiController]
public class StudentsController(ISender sender) : ApiControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet(Name = "GetStudents")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetStudents([FromQuery] StudentParameters studentParameters)
    {
        var linkParams = new LinkStudentParameters(studentParameters, HttpContext);
        var baseResult = await _sender.Send(new GetStudentsQuery(linkParams, TrackChanges: false));
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var (linkResponse, metaData) = baseResult.GetResult<(LinkResponse, MetaData)>();

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

        return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) :
            Ok(linkResponse.ShapedEntities);
    }

    [HttpGet("{id:guid}", Name = "GetStudent")]
    public async Task<IActionResult> GetStudent(Guid id)
    {
        var baseResult = await _sender.Send(new GetStudentQuery(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var products = baseResult.GetResult<StudentDto>();
        return Ok(products);
    }

    [HttpPost(Name = "CreateStudent")]
    public async Task<IActionResult> CreateStudent([FromBody] StudentForCreationDto student)
    {
        var baseResult = await _sender.Send(new CreateStudentCommand(student));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var createdProduct = baseResult.GetResult<StudentDto>();

        return CreatedAtRoute("GetStudent", new { id = createdProduct.Id }, createdProduct);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteStudent(Guid id)
    {
        var baseResult = await _sender.Send(new DeleteStudentCommand(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateStudent(Guid id, [FromBody] StudentForUpdateDto student)
    {
        var baseResult = await _sender.Send(new UpdateStudentCommand(id, student, TrackChanges: true));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

}
