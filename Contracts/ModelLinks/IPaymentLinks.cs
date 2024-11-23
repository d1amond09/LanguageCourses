using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using Microsoft.AspNetCore.Http;

namespace Contracts.ModelLinks;

public interface IPaymentLinks
{
    LinkResponse TryGenerateLinks(
        IEnumerable<PaymentDto> employeesDto,
        string fields,
        HttpContext httpContext);

}
