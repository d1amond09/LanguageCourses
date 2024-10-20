using Contracts;
using Contracts.Services;
using LanguageCourses.Application.Services;

namespace LanguageCourses.Application;

public class ServiceManager(IRepositoryManager repManager, ILoggerManager logger) : IServiceManager
{
	private readonly Lazy<IEmployeeService> _empService = new(() =>
		new EmployeeService(repManager, logger));

	private readonly Lazy<IJobTitleService> _jobService = new(() =>
		new JobTitleService(repManager, logger));

	private readonly Lazy<ICourseService> _coursesService = new(() =>
		new CourseService(repManager, logger));

	private readonly Lazy<IStudentService> _studService = new(() =>
		new StudentService(repManager, logger));

	private readonly Lazy<IPaymentService> _paymService = new(() =>
		new PaymentService(repManager, logger));

	public ICourseService CourseService => _coursesService.Value;
	public IEmployeeService EmployeeService => _empService.Value;
	public IJobTitleService JobTitleService => _jobService.Value;
	public IPaymentService PaymentService => _paymService.Value;
	public IStudentService StudentService => _studService.Value;
}
