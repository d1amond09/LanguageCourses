using System;
using Contracts;
using Contracts.Repositories;
using LanguageCourses.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence;

public class RepositoryManager : IRepositoryManager
{
    public LanguageCoursesContext DbContext => _dbContext;
    private readonly LanguageCoursesContext _dbContext;
    private readonly Lazy<IEmployeeRepository> _empRep;
    private readonly Lazy<IJobTitleRepository> _jobRep;
    private readonly Lazy<ICourseRepository> _coursesRep;
    private readonly Lazy<IStudentRepository> _studRep;
    private readonly Lazy<IPaymentRepository> _paymRep;
    public RepositoryManager(LanguageCoursesContext dbContext)
    {
        _dbContext = dbContext;
        _empRep = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(_dbContext));
        _jobRep = new Lazy<IJobTitleRepository>(() => new JobTitleRepository(_dbContext));
        _coursesRep = new Lazy<ICourseRepository>(() => new CourseRepository(_dbContext));
        _studRep = new Lazy<IStudentRepository>(() => new StudentRepository(_dbContext));
        _paymRep = new Lazy<IPaymentRepository>(() => new PaymentRepository(_dbContext));
    }

    public IEmployeeRepository Employees => _empRep.Value;
    public IJobTitleRepository JobTitles => _jobRep.Value;
    public ICourseRepository Courses => _coursesRep.Value;
    public IStudentRepository Students => _studRep.Value;
    public IPaymentRepository Payments => _paymRep.Value;
    public void SetModified<T>(T entity) where T : class
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
    }
    public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    public void SaveChanges() => _dbContext.SaveChanges();
}
