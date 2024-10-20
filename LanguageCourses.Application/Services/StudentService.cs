using System.Linq.Expressions;
using Contracts;
using Contracts.Repositories;
using Contracts.Services;
using LanguageCourses.Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace LanguageCourses.Application.Services;

internal sealed class StudentService(IRepositoryManager rep, ILoggerManager logger) : IStudentService
{
	private readonly IRepositoryManager _rep = rep;
	private readonly ILoggerManager _logger = logger;

	public async Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges) =>
		await _rep.Students.GetAllStudentsAsync(trackChanges);

	public async Task<Student?> GetStudentAsync(Guid id, bool trackChanges) =>
		await _rep.Students.GetStudentAsync(id, trackChanges);

}