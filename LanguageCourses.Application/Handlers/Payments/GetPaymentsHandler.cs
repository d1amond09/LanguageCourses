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

namespace LanguageCourses.Application.Handlers.Payments;

public class GetPaymentsHandler(IRepositoryManager rep, IMapper mapper, IPaymentLinks courseLinks) :
    IRequestHandler<GetPaymentsQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;
    private readonly IPaymentLinks _courseLinks = courseLinks;

    public async Task<ApiBaseResponse> Handle(GetPaymentsQuery request, CancellationToken cancellationToken)
    {
        if (request.LinkParameters.PaymentParameters.NotValidAmountRange)
            return new ApiMaxAmountRangeBadRequestResponse();

        if (request.LinkParameters.PaymentParameters.NotValidDateRange)
            return new ApiMaxDateRangeBadRequestResponse();

        var paymentsWithMetaData = await _rep.Payments.GetAllPaymentsAsync(
            request.LinkParameters.PaymentParameters,
            request.TrackChanges
        );

        var paymentsDto = _mapper.Map<IEnumerable<PaymentDto>>(paymentsWithMetaData);

        var links = _courseLinks.TryGenerateLinks(
            paymentsDto,
            request.LinkParameters.PaymentParameters.Fields,
            request.LinkParameters.Context
        );

        (LinkResponse, MetaData) result = new(links, paymentsWithMetaData.MetaData);

        return new ApiOkResponse<(LinkResponse, MetaData)>(result);
    }
}
