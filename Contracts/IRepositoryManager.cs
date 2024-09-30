using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.Repositories;

namespace Contracts;

public interface IRepositoryManager
{
	ICourseRepository Courses { get; }
	IEmployeeRepository Employees { get; }
	IJobTitleRepository JobTitles { get; }
	IPaymentRepository Payments { get; }
	IStudentRepository Students { get; }
	Task SaveAsync();
}
