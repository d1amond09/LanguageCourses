using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Queries;

public sealed record GetStudentQuery(Guid StudentId, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

