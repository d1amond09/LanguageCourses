namespace LanguageCourses.Domain.Responses;

public sealed class EmployeeNotFoundResponse(Guid id) : ApiNotFoundResponse($"EMployee with id:{id} not found!")
{

}
