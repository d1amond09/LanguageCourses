using AutoMapper;
using Contracts;
using Contracts.Services;
using LanguageCourses.Domain.DataTransferObjects;

namespace LanguageCourses.Application.Services;

internal sealed class CourseService(IRepositoryManager rep, ILoggerManager logger, IMapper mapper) : ICourseService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;
	private readonly IMapper _mapper = mapper;
	public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync(bool trackChanges)
	{
		var courses = await _rep.Courses.GetAllCoursesAsync(trackChanges);
		var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courses);
		return coursesDto;
	}

	public async Task<CourseDto?> GetCourseAsync(Guid id, bool trackChanges)
	{
		var course = await _rep.Courses.GetCourseAsync(id, trackChanges);
		var courseDto = _mapper.Map<CourseDto>(course);
		return courseDto;
	}
}