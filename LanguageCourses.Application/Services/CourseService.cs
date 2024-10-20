using System.Linq.Expressions;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class CourseService(IRepositoryManager rep, IMemoryCache memoryCache) : ICourseService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly IMemoryCache _cache = memoryCache;
	private int _rowsNumber = 20;

	public IEnumerable<Course> GetCourses()
	{
		return _rep.Courses.GetCoursesTop(_rowsNumber);
	}

	public void AddCourses(string cacheKey)
	{
		IEnumerable<Course> courses = _rep.Courses.GetCoursesTop(_rowsNumber);

		_cache.Set(cacheKey, courses, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public void AddCoursesByCondition(string cacheKey, Expression<Func<Course, bool>> expression)
	{
		IEnumerable<Course> courses = _rep.Courses.FindByCondition(expression).Take(_rowsNumber);

		_cache.Set(cacheKey, courses, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public IEnumerable<Course>? GetCourses(string cacheKey)
	{
		if (!_cache.TryGetValue(cacheKey, out IEnumerable<Course>? courses))
		{
			courses = _rep.Courses.GetCoursesTop(_rowsNumber);
			if (courses != null)
			{
				_cache.Set(cacheKey, courses,
				new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(248)));
			}
		}
		return courses;
	}
}