namespace LanguageCourses.Domain.Responses;

public sealed class ApiMaxAmountRangeBadRequestResponse() :
    ApiBadRequestResponse($"Max amount can't be less than min amount")
{

}
