using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Queries;

public sealed record GetJobTitleQuery(Guid JobTitleId, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

