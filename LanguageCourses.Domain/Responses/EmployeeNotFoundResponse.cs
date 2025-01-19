namespace LanguageCourses.Domain.Responses;

public sealed class EmployeeNotFoundResponse(Guid id) : ApiNotFoundResponse($"Employee with id:{id} not found!")
{

}
