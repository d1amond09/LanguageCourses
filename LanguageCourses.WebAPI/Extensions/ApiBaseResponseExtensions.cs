using LanguageCourses.Domain.Responses;

namespace LanguageCourses.WebAPI.Extensions;

public static class ApiBaseResponseExtensions
{
    public static TResultType GetResult<TResultType>(this ApiBaseResponse apiBaseResponse) =>
        ((ApiOkResponse<TResultType>)apiBaseResponse).Result;

}
