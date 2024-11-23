using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts.ModelLinks;

public interface IStudentLinks
{
    LinkResponse TryGenerateLinks(
        IEnumerable<StudentDto> employeesDto,
        string fields,
        HttpContext httpContext);

}
