using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.Models;
using Microsoft.Net.Http.Headers;

namespace LanguageCourses.WebAPI.Utility;

public class StudentLinks(LinkGenerator linkGenerator, IDataShaper<StudentDto> dataShaper) : IStudentLinks
{
    private readonly LinkGenerator _linkGenerator = linkGenerator;
    private readonly IDataShaper<StudentDto> _dataShaper = dataShaper;

    public LinkResponse TryGenerateLinks(IEnumerable<StudentDto> studentsDto, string fields, HttpContext httpContext)
    {
        var shapedStudents = ShapeData(studentsDto, fields);

        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedStudents(studentsDto, fields, httpContext, shapedStudents);

        return ReturnShapedStudents(shapedStudents);
    }

    private LinkResponse ReturnShapedStudents(List<Entity> shapedStudents) =>
        new() { ShapedEntities = shapedStudents };

    private List<Entity> ShapeData(IEnumerable<StudentDto> studentsDto, string fields) =>
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
    private LinkResponse ReturnLinkdedStudents(IEnumerable<StudentDto> studentsDto, string fields, HttpContext httpContext, List<Entity> shapedStudents)
    {
        var studentDtoList = studentsDto.ToList();

        for (var index = 0; index < studentDtoList.Count; index++)
        {
            var studentLinks = CreateLinksForStudents(httpContext, studentDtoList[index].Id, fields);
            shapedStudents[index].Add("Links", studentLinks);
        }

        var studentCollection = new LinkCollectionWrapper<Entity>(shapedStudents);
        var linkedStudents = CreateLinksForStudents(httpContext, studentCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedStudents };
    }

    private List<Link> CreateLinksForStudents(HttpContext httpContext, Guid id, string fields = "")
    {
        List<Link> links = [
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetStudent", values: new { id, fields })!, "self", "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteStudent", values: new { id })!, "delete_student", "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateStudent", values: new { id })!, "update_student", "PUT")
        ];

        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForStudents(HttpContext httpContext, LinkCollectionWrapper<Entity> studentsWrapper)
    {
        studentsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetStudent", values: new { })!, "self", "GET"));
        return studentsWrapper;
    }
}
