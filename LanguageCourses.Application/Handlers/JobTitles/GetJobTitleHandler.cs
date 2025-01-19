using AutoMapper;
using Contracts;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.JobTitles;

public class GetJobTitleHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<GetJobTitleQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(GetJobTitleQuery request, CancellationToken cancellationToken)
    {
        var jobTitle = await _rep.JobTitles.GetJobTitleAsync(request.JobTitleId, request.TrackChanges);
        if (jobTitle is null)
            return new JobTitleNotFoundResponse(request.JobTitleId);

        var jobTitleDto = _mapper.Map<JobTitleDto>(jobTitle);
        return new ApiOkResponse<JobTitleDto>(jobTitleDto);
    }
}
