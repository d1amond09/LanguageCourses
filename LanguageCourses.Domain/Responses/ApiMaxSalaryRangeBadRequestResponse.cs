namespace LanguageCourses.Domain.Responses;

public sealed class ApiMaxSalaryRangeBadRequestResponse() :
    ApiBadRequestResponse($"Max salary can't be less than min salary")
{

}
