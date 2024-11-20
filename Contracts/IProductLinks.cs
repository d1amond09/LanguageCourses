using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts;

public interface ICourseLinks
{
    LinkResponse TryGenerateLinks(
        IEnumerable<CourseDto> employeesDto,
        string fields,
        HttpContext httpContext);

}
