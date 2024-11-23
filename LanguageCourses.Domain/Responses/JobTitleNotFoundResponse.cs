namespace LanguageCourses.Domain.Responses;

public sealed class JobTitleNotFoundResponse(Guid id) : ApiNotFoundResponse($"JobTitle with id:{id} not found!")
{

}
