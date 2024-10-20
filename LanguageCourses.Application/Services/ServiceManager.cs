using Contracts.Repositories;
using Contracts.Services;

namespace LanguageCourses.Application.Services;

public class ServiceManager : IServiceManager
{
	private readonly Lazy<IEmployeeService> _empService;
	private readonly Lazy<IJobTitleService> _jobService;
	private readonly Lazy<ICourseService> _coursesService;
	private readonly Lazy<IStudentService> _studService;
	private readonly Lazy<IPaymentService> _paymService;

    public ServiceManager(IRepositoryManager repManager)
    {
		_empService = new Lazy<IEmployeeService>(() => new EmployeeService(repManager));
		_jobService = new Lazy<IJobTitleService>(() => new JobTitleService(repManager));
		_coursesService = new Lazy<ICourseService>(() => new CourseService(repManager));
		//_studService = new Lazy<IStudentService>(() => new StudentService(repManager));
		_paymService = new Lazy<IPaymentService>(() => new PaymentService(repManager));
    }


    public ICourseService CourseService => _coursesService.Value;
	public IEmployeeService EmployeeService => _empService.Value;
	public IJobTitleService JobTitleService => _jobService.Value;
	public IPaymentService PaymentService => _paymService.Value;
	public IStudentService StudentService => _studService.Value;
}
