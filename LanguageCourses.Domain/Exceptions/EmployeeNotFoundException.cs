namespace LanguageCourses.Domain.Exceptions;

public class EmployeeNotFoundException(Guid id) :
    NotFoundException($"The employee with id: {id} doesn't exist in the database.")
{

}
