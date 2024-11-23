using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.Models;
using Microsoft.Net.Http.Headers;

namespace LanguageCourses.WebAPI.Utility;

public class EmployeeLinks(LinkGenerator linkGenerator, IDataShaper<EmployeeDto> dataShaper) : IEmployeeLinks
{
    private readonly LinkGenerator _linkGenerator = linkGenerator;
    private readonly IDataShaper<EmployeeDto> _dataShaper = dataShaper;

    public LinkResponse TryGenerateLinks(IEnumerable<EmployeeDto> employeesDto, string fields, HttpContext httpContext)
    {
        var shapedEmployees = ShapeData(employeesDto, fields);

        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedEmployees(employeesDto, fields, httpContext, shapedEmployees);

        return ReturnShapedEmployees(shapedEmployees);
    }

    private LinkResponse ReturnShapedEmployees(List<Entity> shapedEmployees) =>
        new() { ShapedEntities = shapedEmployees };

    private List<Entity> ShapeData(IEnumerable<EmployeeDto> employeesDto, string fields) =>
        _dataShaper.ShapeData(employeesDto, fields)
            .Select(e => e.Entity)
            .ToList();

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue?)httpContext.Items["AcceptHeaderMediaType"];
        ArgumentNullException.ThrowIfNull(mediaType);
        return mediaType.SubTypeWithoutSuffix
            .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    private LinkResponse ReturnLinkdedEmployees(IEnumerable<EmployeeDto> employeesDto, string fields, HttpContext httpContext, List<Entity> shapedEmployees)
    {
        var employeeDtoList = employeesDto.ToList();

        for (var index = 0; index < employeeDtoList.Count; index++)
        {
            var employeeLinks = CreateLinksForEmployees(httpContext, employeeDtoList[index].Id, fields);
            shapedEmployees[index].Add("Links", employeeLinks);
        }

        var employeeCollection = new LinkCollectionWrapper<Entity>(shapedEmployees);
        var linkedEmployees = CreateLinksForEmployees(httpContext, employeeCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedEmployees };
    }

    private List<Link> CreateLinksForEmployees(HttpContext httpContext, Guid id, string fields = "")
    {
        List<Link> links = [
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployee", values: new { id, fields })!, "self", "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeleteEmployee", values: new { id })!, "delete_employee", "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdateEmployee", values: new { id })!, "update_employee", "PUT")
        ];

        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForEmployees(HttpContext httpContext, LinkCollectionWrapper<Entity> employeesWrapper)
    {
        employeesWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetEmployee", values: new { })!, "self", "GET"));
        return employeesWrapper;
    }
}
