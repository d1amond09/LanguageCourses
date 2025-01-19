using System.Linq.Expressions;
using Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal abstract class RepositoryBase<T>(LanguageCoursesContext appDbContext) : IRepositoryBase<T> where T : class
{
    protected LanguageCoursesContext _appDbContext = appDbContext;

    public IQueryable<T> FindAll(bool trackChanges = false) =>
        !trackChanges ? _appDbContext.Set<T>()
        .AsNoTracking() : _appDbContext.Set<T>();

    public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
        !trackChanges ? _appDbContext.Set<T>().Where(expression)
        .AsNoTracking() : _appDbContext.Set<T>().Where(expression);

    public void Attach(T entity) => _appDbContext.Set<T>().Attach(entity);
    public void Create(T entity) => _appDbContext.Set<T>().Add(entity);
    public void Update(T entity) => _appDbContext.Set<T>().Update(entity);
    public void Delete(T entity) => _appDbContext.Set<T>().Remove(entity);
}
