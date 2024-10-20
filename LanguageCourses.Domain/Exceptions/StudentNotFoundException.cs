using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.Exceptions;

public class StudentNotFoundException(Guid studentId) : 
	NotFoundException($"The student with id: {studentId} doesn't exist in the database.")
{

}
