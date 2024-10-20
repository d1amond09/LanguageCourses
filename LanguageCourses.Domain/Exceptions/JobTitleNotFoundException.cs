using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.Exceptions;

public class JobTitleNotFoundException(Guid id) : 
	NotFoundException($"The jobtitle with id: {id} doesn't exist in the database.")
{

}
