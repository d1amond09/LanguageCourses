using Contracts;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.Models;
using Microsoft.Net.Http.Headers;

namespace LanguageCourses.WebAPI.Utility;

public class CourseLinks(LinkGenerator linkGenerator, IDataShaper<CourseDto> dataShaper) : ICourseLinks
{
    private readonly LinkGenerator _linkGenerator = linkGenerator;
    private readonly IDataShaper<CourseDto> _dataShaper = dataShaper;

    public LinkResponse TryGenerateLinks(IEnumerable<CourseDto> coursesDto, string fields, HttpContext httpContext)
    {
        var shapedCourses = ShapeData(coursesDto, fields);

        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedCourses(coursesDto, fields, httpContext, shapedCourses);

        return ReturnShapedCourses(shapedCourses);
    }

    private LinkResponse ReturnShapedCourses(List<Entity> shapedCourses) =>
        new() { ShapedEntities = shapedCourses };

    private List<Entity> ShapeData(IEnumerable<CourseDto> coursesDto, string fields) =>
        _dataShaper.ShapeData(coursesDto, fields)
            .Select(e => e.Entity)
            .ToList();

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue?)httpContext.Items["AcceptHeaderMediaType"];
        ArgumentNullException.ThrowIfNull(mediaType);
        return mediaType.SubTypeWithoutSuffix
            .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    private LinkResponse ReturnLinkdedCourses(IEnumerable<CourseDto> coursesDto, string fields, HttpContext httpContext, List<Entity> shapedCourses)
    {
        var courseDtoList = coursesDto.ToList();

        for (var index = 0; index < courseDtoList.Count; index++)
        {
            var courseLinks = CreateLinksForCourses(httpContext, courseDtoList[index].Id, fields);
            shapedCourses[index].Add("Links", courseLinks);
        }

        var courseCollection = new LinkCollectionWrapper<Entity>(shapedCourses);
        var linkedCourses = CreateLinksForCourses(httpContext, courseCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedCourses };
    }

    private List<Link> CreateLinksForCourses(HttpContext httpContext, Guid id, string fields = "")
    {
        List<Link> links = [
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetCourse", values: new { id, fields })!, "self", "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteCourse", values: new { id })!, "delete_course", "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateCourse", values: new { id })!, "update_course", "PUT")
        ];

        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForCourses(HttpContext httpContext, LinkCollectionWrapper<Entity> coursesWrapper)
    {
        coursesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetCourse", values: new { })!, "self", "GET"));
        return coursesWrapper;
    }
}
