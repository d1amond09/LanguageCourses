using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record DeleteCourseCommand(Guid Id, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

