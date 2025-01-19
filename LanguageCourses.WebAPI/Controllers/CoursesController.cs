using System.Text.Json;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.WebAPI.ActionFilters;
using LanguageCourses.WebAPI.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[Route("api/courses")]
[ApiController]
[Authorize]
public class CoursesController(ISender sender) : ApiControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet(Name = "GetCourses")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetCourses([FromQuery] CourseParameters courseParameters)
    {
        var linkParams = new LinkCourseParameters(courseParameters, HttpContext);
        var baseResult = await _sender.Send(new GetCoursesQuery(linkParams, TrackChanges: false));
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var (linkResponse, metaData) = baseResult.GetResult<(LinkResponse, MetaData)>();

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

        return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) :
            Ok(linkResponse.ShapedEntities);
    }

    [HttpGet("{id:guid}", Name = "GetCourse")]
    public async Task<IActionResult> GetCourse(Guid id)
    {
        var baseResult = await _sender.Send(new GetCourseQuery(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var products = baseResult.GetResult<CourseDto>();
        return Ok(products);
    }

    [HttpPost(Name = "CreateCourse")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateCourse([FromBody] CourseForCreationDto course)
    {
        var baseResult = await _sender.Send(new CreateCourseCommand(course));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var createdProduct = baseResult.GetResult<CourseDto>();

        return CreatedAtRoute("GetCourse", new { id = createdProduct.Id }, createdProduct);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteCourse(Guid id)
    {
        var baseResult = await _sender.Send(new DeleteCourseCommand(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateCourse(Guid id, [FromBody] CourseForUpdateDto course)
    {
        var baseResult = await _sender.Send(new UpdateCourseCommand(id, course, TrackChanges: true));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }
}
