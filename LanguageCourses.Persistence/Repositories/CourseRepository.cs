using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LanguageCourses.Persistence.Repositories;

public class CourseRepository(LanguageCoursesContext appDbContext) :
	RepositoryBase<Course>(appDbContext), ICourseRepository
{
	public void CreateCourse(Course Course) => Create(Course);

	public void DeleteCourse(Course Course) => Delete(Course);

	public async Task<IEnumerable<Course>> GetAllCoursesAsync(bool trackChanges = false) =>
		await FindAll(trackChanges)
			.Include(x => x.Employee)
			.Include(c => c.Students)
			.OrderBy(c => c.Name)
			.ToListAsync();
	public async Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges = false) =>
		await FindByCondition(c => c.CourseId.Equals(courseId), trackChanges)
			.SingleOrDefaultAsync();

	public async Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
		await FindByCondition(x => ids.Contains(x.CourseId), trackChanges)
			.ToListAsync();

	public IEnumerable<Course> GetCoursesTop(int rows) =>
		 [.. FindAll().Include(x => x.Employee).Include(c => c.Students).Where(c => c.Students.Count > 0).Take(rows)];
}
