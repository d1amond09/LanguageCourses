﻿using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;

namespace Contracts.Repositories;

public interface IStudentRepository
{
	Task<IEnumerable<Student>> GetAllStudentsAsync(bool trackChanges);
	IEnumerable<Student> GetStudentsTop(int rows);
	Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
	Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges);
	Task<Student?> GetStudentByPassportAsync(string passport, bool trackChanges = false);
	Student? GetStudentByPassport(string passport, bool trackChanges = false);
	IQueryable<Student> FindByCondition(Expression<Func<Student, bool>> expression, bool trackChanges = false);
	void CreateStudent(Student student);
	void DeleteStudent(Student student);
	void UpdateStudent(Student student);
}
