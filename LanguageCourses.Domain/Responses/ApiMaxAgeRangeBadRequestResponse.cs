namespace LanguageCourses.Domain.Responses;

public sealed class ApiMaxAgeRangeBadRequestResponse() :
    ApiBadRequestResponse($"Max age can't be less than min age")
{

}
