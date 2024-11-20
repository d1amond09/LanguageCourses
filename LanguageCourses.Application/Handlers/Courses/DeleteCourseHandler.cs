using AutoMapper;
using Contracts;
using Contracts.Repositories;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Courses;

public sealed class DeleteCourseHandler(IRepositoryManager rep) : IRequestHandler<DeleteCourseCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;

    public async Task<ApiBaseResponse> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = await _rep.Courses.GetCourseAsync(request.Id, request.TrackChanges);

        if (course is null)
            return new CourseNotFoundResponse(request.Id);

        _rep.Courses.DeleteCourse(course);
        await _rep.SaveAsync();

        return new ApiOkResponse<Course>(course);
    }
}
