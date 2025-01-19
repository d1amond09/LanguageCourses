using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record UpdateJobTitleCommand(Guid Id, JobTitleForUpdateDto JobTitle, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

