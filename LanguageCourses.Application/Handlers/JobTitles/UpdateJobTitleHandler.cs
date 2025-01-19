using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.JobTitles;

public sealed class UpdateJobTitleHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<UpdateJobTitleCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(UpdateJobTitleCommand request, CancellationToken cancellationToken)
    {
        var returnEntity = await _rep.JobTitles.GetJobTitleAsync(request.Id, request.TrackChanges);

        if (returnEntity is null)
            return new JobTitleNotFoundResponse(request.Id);

        _mapper.Map(request.JobTitle, returnEntity);
        await _rep.SaveAsync();

        return new ApiOkResponse<JobTitle>(returnEntity);
    }
}
