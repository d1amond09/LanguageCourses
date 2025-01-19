using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.Models;
using Microsoft.Net.Http.Headers;

namespace LanguageCourses.WebAPI.Utility;

public class JobTitleLinks(LinkGenerator linkGenerator, IDataShaper<JobTitleDto> dataShaper) : IJobTitleLinks
{
    private readonly LinkGenerator _linkGenerator = linkGenerator;
    private readonly IDataShaper<JobTitleDto> _dataShaper = dataShaper;

    public LinkResponse TryGenerateLinks(IEnumerable<JobTitleDto> studentsDto, string fields, HttpContext httpContext)
    {
        var shapedJobTitles = ShapeData(studentsDto, fields);

        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedJobTitles(studentsDto, fields, httpContext, shapedJobTitles);

        return ReturnShapedJobTitles(shapedJobTitles);
    }

    private LinkResponse ReturnShapedJobTitles(List<Entity> shapedJobTitles) =>
        new() { ShapedEntities = shapedJobTitles };

    private List<Entity> ShapeData(IEnumerable<JobTitleDto> studentsDto, string fields) =>
        _dataShaper.ShapeData(studentsDto, fields)
            .Select(e => e.Entity)
            .ToList();

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue?)httpContext.Items["AcceptHeaderMediaType"];
        ArgumentNullException.ThrowIfNull(mediaType);
        return mediaType.SubTypeWithoutSuffix
            .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    private LinkResponse ReturnLinkdedJobTitles(IEnumerable<JobTitleDto> studentsDto, string fields, HttpContext httpContext, List<Entity> shapedJobTitles)
    {
        var studentDtoList = studentsDto.ToList();

        for (var index = 0; index < studentDtoList.Count; index++)
        {
            var studentLinks = CreateLinksForJobTitles(httpContext, studentDtoList[index].Id, fields);
            shapedJobTitles[index].Add("Links", studentLinks);
        }

        var studentCollection = new LinkCollectionWrapper<Entity>(shapedJobTitles);
        var linkedJobTitles = CreateLinksForJobTitles(httpContext, studentCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedJobTitles };
    }

    private List<Link> CreateLinksForJobTitles(HttpContext httpContext, Guid id, string fields = "")
    {
        List<Link> links = [
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetJobTitle", values: new { id, fields })!, "self", "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteJobTitle", values: new { id })!, "delete_student", "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateJobTitle", values: new { id })!, "update_student", "PUT")
        ];

        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForJobTitles(HttpContext httpContext, LinkCollectionWrapper<Entity> studentsWrapper)
    {
        studentsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetJobTitle", values: new { })!, "self", "GET"));
        return studentsWrapper;
    }
}
