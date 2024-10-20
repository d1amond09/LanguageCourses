using Contracts.Repositories;
using Contracts.Services;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

public class ServiceManager(IRepositoryManager repManager, IMemoryCache memCache) : IServiceManager
{
	private readonly Lazy<IEmployeeService> _empService = new(() => 
		new EmployeeService(repManager, memCache));

	private readonly Lazy<IJobTitleService> _jobService = new(() => 
		new JobTitleService(repManager, memCache));

	private readonly Lazy<ICourseService> _coursesService = new(() => 
		new CourseService(repManager, memCache));

	private readonly Lazy<IStudentService> _studService = new(() => 
		new StudentService(repManager, memCache));

	private readonly Lazy<IPaymentService> _paymService = new(() => 
		new PaymentService(repManager, memCache));

	public ICourseService CourseService => _coursesService.Value;
	public IEmployeeService EmployeeService => _empService.Value;
	public IJobTitleService JobTitleService => _jobService.Value;
	public IPaymentService PaymentService => _paymService.Value;
	public IStudentService StudentService => _studService.Value;
}
