namespace LanguageCourses.Domain.Responses;

public sealed class ApiMaxTuitionFeeRangeBadRequestResponse() :
    ApiBadRequestResponse($"Max price can't be less than min price")
{

}
