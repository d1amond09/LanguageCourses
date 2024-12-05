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

[Route("api/jobtitles")]
[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[ApiController]
[Authorize]
public class JobTitlesController(ISender sender) : ApiControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet(Name = "GetJobTitles")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetJobTitles([FromQuery] JobTitleParameters jobTitleParameters)
    {
        var linkParams = new LinkJobTitleParameters(jobTitleParameters, HttpContext);
        var baseResult = await _sender.Send(new GetJobTitlesQuery(linkParams, TrackChanges: false));
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var (linkResponse, metaData) = baseResult.GetResult<(LinkResponse, MetaData)>();

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

        return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) :
            Ok(linkResponse.ShapedEntities);
    }

    [HttpGet("{id:guid}", Name = "GetJobTitle")]
    public async Task<IActionResult> GetJobTitle(Guid id)
    {
        var baseResult = await _sender.Send(new GetJobTitleQuery(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var products = baseResult.GetResult<JobTitleDto>();
        return Ok(products);
    }

    [HttpPost(Name = "CreateJobTitle")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateJobTitle([FromBody] JobTitleForCreationDto jobTitle)
    {
        var baseResult = await _sender.Send(new CreateJobTitleCommand(jobTitle));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var createdProduct = baseResult.GetResult<JobTitleDto>();

        return CreatedAtRoute("GetJobTitle", new { id = createdProduct.Id }, createdProduct);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteJobTitle(Guid id)
    {
        var baseResult = await _sender.Send(new DeleteJobTitleCommand(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateJobTitle(Guid id, [FromBody] JobTitleForUpdateDto jobTitle)
    {
        var baseResult = await _sender.Send(new UpdateJobTitleCommand(id, jobTitle, TrackChanges: true));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

}
