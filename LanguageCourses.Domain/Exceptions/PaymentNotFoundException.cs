using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCourses.Domain.Exceptions;

public class PaymentNotFoundException(Guid id) : 
	NotFoundException($"The payment with id: {id} doesn't exist in the database.")
{

}
