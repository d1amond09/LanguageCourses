using AutoMapper;
using Contracts;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Payments;

public class GetPaymentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<GetPaymentQuery, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var payment = await _rep.Payments.GetPaymentAsync(request.PaymentId, request.TrackChanges);
        if (payment is null)
            return new PaymentNotFoundResponse(request.PaymentId);

        var paymentDto = _mapper.Map<PaymentDto>(payment);
        return new ApiOkResponse<PaymentDto>(paymentDto);
    }
}
