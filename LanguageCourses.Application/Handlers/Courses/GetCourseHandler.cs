using AutoMapper;
using Contracts;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Courses;

public class GetCourseHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<GetCourseQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(GetCourseQuery request, CancellationToken cancellationToken)
    {
        var course = await _rep.Courses.GetCourseAsync(request.CourseId, request.TrackChanges);
        if (course is null)
            return new CourseNotFoundResponse(request.CourseId);

        var courseDto = _mapper.Map<CourseDto>(course);
        return new ApiOkResponse<CourseDto>(courseDto);
    }
}
