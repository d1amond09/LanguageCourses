using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.JobTitles;

public sealed class DeleteJobTitleHandler(IRepositoryManager rep) : IRequestHandler<DeleteJobTitleCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;

    public async Task<ApiBaseResponse> Handle(DeleteJobTitleCommand request, CancellationToken cancellationToken)
    {
        var jobTitle = await _rep.JobTitles.GetJobTitleAsync(request.Id, request.TrackChanges);

        if (jobTitle is null)
            return new JobTitleNotFoundResponse(request.Id);

        _rep.JobTitles.DeleteJobTitle(jobTitle);
        await _rep.SaveAsync();

        return new ApiOkResponse<JobTitle>(jobTitle);
    }
}
