using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts.ModelLinks;

public interface ICourseLinks
{
    LinkResponse TryGenerateLinks(
        IEnumerable<CourseDto> coursesDto,
        string fields,
        HttpContext httpContext);

}
