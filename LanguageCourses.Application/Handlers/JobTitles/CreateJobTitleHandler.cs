using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.JobTitles;

public sealed class CreateJobTitleHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<CreateJobTitleCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(CreateJobTitleCommand request, CancellationToken cancellationToken)
    {
        var entityToCreate = _mapper.Map<JobTitle>(request.JobTitle);

        _rep.JobTitles.CreateJobTitle(entityToCreate);
        await _rep.SaveAsync();

        var entityToReturn = _mapper.Map<JobTitleDto>(entityToCreate);
        return new ApiOkResponse<JobTitleDto>(entityToReturn);
    }
}
