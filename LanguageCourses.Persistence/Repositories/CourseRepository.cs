﻿using Contracts.Repositories;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
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
             FindAll(trackChanges)
                .FilterTuitionFeeCourses(courseParameters.MinTuitionFee, courseParameters.MaxTuitionFee)
                .FilterHoursCourses(courseParameters.MinHours, courseParameters.MaxHours)
                .SearchByTrainingProgram(courseParameters.SearchTrainingProgram)
                .Search(courseParameters.SearchTerm)
                .Sort(courseParameters.OrderBy);

        var count = await courses.CountAsync();



        var coursesToReturn = await courses
                .Skip((courseParameters.PageNumber - 1) * courseParameters.PageSize)
                .Take(courseParameters.PageSize)
                .ToListAsync();


        return new PagedList<Course>(
            coursesToReturn,
            count,
            courseParameters.PageNumber,
            courseParameters.PageSize
        );
    }
    public async Task<Course?> GetCourseAsync(Guid courseId, bool trackChanges = false) =>
        await FindByCondition(c => c.Id.Equals(courseId), trackChanges).Include(x => x.Students)
            .SingleOrDefaultAsync();

    public async Task<IEnumerable<Course>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges = false) =>
        await FindByCondition(x => ids.Contains(x.Id), trackChanges).Include(x => x.Students)
            .ToListAsync();
}
