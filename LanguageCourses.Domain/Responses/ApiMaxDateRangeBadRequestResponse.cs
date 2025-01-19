namespace LanguageCourses.Domain.Responses;

public sealed class ApiMaxDateRangeBadRequestResponse() :
    ApiBadRequestResponse($"Max date can't be less than min date")
{

}
