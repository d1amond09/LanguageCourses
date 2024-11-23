namespace LanguageCourses.Domain.Responses;

public sealed class ApiMaxHoursRangeBadRequestResponse() :
    ApiBadRequestResponse($"Max hours can't be less than min hours")
{

}
