using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Queries;

public sealed record GetCourseQuery(Guid CourseId, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

