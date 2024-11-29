using AutoMapper;
using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Students;

public class GetStudentsHandler(IRepositoryManager rep, IMapper mapper, IStudentLinks courseLinks) :
    IRequestHandler<GetStudentsQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;
    private readonly IStudentLinks _courseLinks = courseLinks;

    public async Task<ApiBaseResponse> Handle(GetStudentsQuery request, CancellationToken cancellationToken)
    {
        if (request.LinkParameters.StudentParameters.NotValidBirthDateRange)
            return new ApiMaxDateRangeBadRequestResponse();

        if (request.LinkParameters.StudentParameters.NotValidAgeRange)
            return new ApiMaxAgeRangeBadRequestResponse();

        var coursesWithMetaData = await _rep.Students.GetAllStudentsAsync(
            request.LinkParameters.StudentParameters,
            request.TrackChanges
        );

        var coursesDto = _mapper.Map<IEnumerable<StudentDto>>(coursesWithMetaData);

        var links = _courseLinks.TryGenerateLinks(
            coursesDto,
            request.LinkParameters.StudentParameters.Fields,
            request.LinkParameters.Context
        );

        (LinkResponse, MetaData) result = new(links, coursesWithMetaData.MetaData);

        return new ApiOkResponse<(LinkResponse, MetaData)>(result);
    }
}
