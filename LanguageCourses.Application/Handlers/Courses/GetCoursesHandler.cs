using AutoMapper;
using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Courses;

public class GetCoursesHandler(IRepositoryManager rep, IMapper mapper, ICourseLinks courseLinks) :
    IRequestHandler<GetCoursesQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;
    private readonly ICourseLinks _courseLinks = courseLinks;

    public async Task<ApiBaseResponse> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        if (request.LinkParameters.CourseParameters.NotValidTuitionFeeRange)
            return new ApiMaxTuitionFeeRangeBadRequestResponse();

        if (request.LinkParameters.CourseParameters.NotValidHoursRange)
            return new ApiMaxAgeRangeBadRequestResponse();

        var coursesWithMetaData = await _rep.Courses.GetAllCoursesAsync(
            request.LinkParameters.CourseParameters,
            request.TrackChanges
        );

        var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(coursesWithMetaData);

        var links = _courseLinks.TryGenerateLinks(
            coursesDto,
            request.LinkParameters.CourseParameters.Fields,
            request.LinkParameters.Context
        );

        (LinkResponse, MetaData) result = new(links, coursesWithMetaData.MetaData);

        return new ApiOkResponse<(LinkResponse, MetaData)>(result);
    }
}
