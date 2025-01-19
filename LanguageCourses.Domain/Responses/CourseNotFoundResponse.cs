namespace LanguageCourses.Domain.Responses;

public sealed class CourseNotFoundResponse(Guid id) : ApiNotFoundResponse($"Course with id:{id} not found!")
{

}
