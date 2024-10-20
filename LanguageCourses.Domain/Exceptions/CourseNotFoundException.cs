namespace LanguageCourses.Domain.Exceptions;

public class CourseNotFoundException(Guid id) :
	NotFoundException($"The course with id: {id} doesn't exist in the database.")
{

}
