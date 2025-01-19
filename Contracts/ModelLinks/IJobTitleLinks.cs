using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts.ModelLinks;

public interface IJobTitleLinks
{
    LinkResponse TryGenerateLinks(
        IEnumerable<JobTitleDto> employeesDto,
        string fields,
        HttpContext httpContext);

}
