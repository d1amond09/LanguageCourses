using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record UpdateCourseCommand(Guid Id, CourseForUpdateDto Course, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

