namespace LanguageCourses.Domain.Responses;

public sealed class PaymentNotFoundResponse(Guid id) : ApiNotFoundResponse($"Payment with id:{id} not found!")
{

}
