﻿using System.Linq.Expressions;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;

namespace Contracts.Repositories;

public interface IStudentRepository
{
    Task<PagedList<Student>> GetAllStudentsAsync(StudentParameters studentParameters, bool trackChanges = false);
    IQueryable<Student> FindByCondition(Expression<Func<Student, bool>> expression, bool trackChanges = false);
    Task<IEnumerable<Student>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task UpdateStudentCoursesAsync(Student student, List<Guid> newCourseIds);
    Task<Student?> GetStudentAsync(Guid studentId, bool trackChanges);
    void CreateStudent(Student student);
    void DeleteStudent(Student student);
    void UpdateStudent(Student student);
}
