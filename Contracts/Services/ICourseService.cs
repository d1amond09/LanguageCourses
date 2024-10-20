﻿using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Services;

public interface ICourseService
{
	public IEnumerable<Course> GetCourses();

	public void AddCourses(string cacheKey);

	public IEnumerable<Course>? GetCourses(string cacheKey);

	public void AddCoursesByCondition(string cacheKey, Expression<Func<Course, bool>> expression);
}
