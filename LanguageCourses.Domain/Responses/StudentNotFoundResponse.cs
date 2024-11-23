namespace LanguageCourses.Domain.Responses;

public sealed class StudentNotFoundResponse(Guid id) : ApiNotFoundResponse($"Student with id:{id} not found!")
{

}
