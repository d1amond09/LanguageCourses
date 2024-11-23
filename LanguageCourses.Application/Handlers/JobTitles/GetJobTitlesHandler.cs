using System.ComponentModel.Design;
using System.Dynamic;
using AutoMapper;
using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.JobTitles;

public class GetJobTitlesHandler(IRepositoryManager rep, IMapper mapper, IJobTitleLinks courseLinks) :
    IRequestHandler<GetJobTitlesQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;
    private readonly IJobTitleLinks _courseLinks = courseLinks;

    public async Task<ApiBaseResponse> Handle(GetJobTitlesQuery request, CancellationToken cancellationToken)
    {
        if (request.LinkParameters.JobTitleParameters.NotValidSalaryRange)
            return new ApiMaxSalaryRangeBadRequestResponse();

        var coursesWithMetaData = await _rep.JobTitles.GetAllJobTitlesAsync(
            request.LinkParameters.JobTitleParameters,
            request.TrackChanges
        );

        var coursesDto = _mapper.Map<IEnumerable<JobTitleDto>>(coursesWithMetaData);

        var links = _courseLinks.TryGenerateLinks(
            coursesDto,
            request.LinkParameters.JobTitleParameters.Fields,
            request.LinkParameters.Context
        );

        (LinkResponse, MetaData) result = new(links, coursesWithMetaData.MetaData);

        return new ApiOkResponse<(LinkResponse, MetaData)>(result);
    }
}
