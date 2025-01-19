using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts.ModelLinks;

public interface IEmployeeLinks
{
    LinkResponse TryGenerateLinks(
        IEnumerable<EmployeeDto> employeesDto,
        string fields,
        HttpContext httpContext);

}
