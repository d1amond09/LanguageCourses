using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

internal class CourseRepository(LanguageCoursesContext appDbContext) :
    RepositoryBase<Course>(appDbContext), ICourseRepository
{
    public void CreateCourse(Course Course) => Create(Course);

    public void DeleteCourse(Course Course) => Delete(Course);

    public async Task<PagedList<Course>> GetAllCoursesAsync(CourseParameters courseParameters, bool trackChanges = false)
    {
        var courses =
            await FindAll(trackChanges)
                .FilterCourses(courseParameters.MinTuitionFee, courseParameters.MaxTuitionFee)
                .Search(courseParameters.SearchTerm)
                .Sort(courseParameters.OrderBy)
                .Skip((courseParameters.PageNumber - 1) * courseParameters.PageSize)
                .Take(courseParameters.PageSize)
                .ToListAsync();

        var count = await FindAll(trackChanges).CountAsync();

        return new PagedList<Course>(
            courses,
            count,
            courseParameters.PageNumber,
            courseParameters.PageSize
        );
    }
    public async Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(courseId), trackChanges)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();
}
