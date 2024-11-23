using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Courses;

public sealed class UpdateEmployeeHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {
        var courseEntity = await _rep.Courses.GetCourseAsync(request.Id, request.TrackChanges);

        if (courseEntity is null)
            return new CourseNotFoundResponse(request.Id);

        _mapper.Map(request.Course, courseEntity);
        await _rep.SaveAsync();

        return new ApiOkResponse<Course>(courseEntity);
    }
}
