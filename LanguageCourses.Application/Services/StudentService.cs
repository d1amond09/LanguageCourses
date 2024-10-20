using System.Linq.Expressions;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;


public sealed class StudentService(IRepositoryManager rep, IMemoryCache memoryCache) : IStudentService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly IMemoryCache _cache = memoryCache;
	private int _rowsNumber = 20;

	public IEnumerable<Student> GetStudents()
	{
		return _rep.Students.GetStudentsTop(_rowsNumber);
	}

	public void AddStudents(string cacheKey)
	{
		IEnumerable<Student> students = _rep.Students.GetStudentsTop(_rowsNumber);

		_cache.Set(cacheKey, students, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}
	
	public void AddStudentsByCondition(string cacheKey, Expression<Func<Student, bool>> expression)
	{
		IEnumerable<Student> students = _rep.Students.FindByCondition(expression).Take(_rowsNumber);

		_cache.Set(cacheKey, students, new MemoryCacheEntryOptions
		{
			AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(248)
		});
	}

	public IEnumerable<Student>? GetStudents(string cacheKey)
	{
		if (!_cache.TryGetValue(cacheKey, out IEnumerable<Student>? students))
		{
			students = _rep.Students.GetStudentsTop(_rowsNumber);
			if (students != null)
			{
				_cache.Set(cacheKey, students,
				new MemoryCacheEntryOptions()
					.SetAbsoluteExpiration(TimeSpan.FromSeconds(248)));
			}
		}
		return students;
	}
}