namespace LanguageCourses.Domain.Exceptions;

public class StudentNotFoundException(Guid studentId) :
	NotFoundException($"The student with id: {studentId} doesn't exist in the database.")
{

}
