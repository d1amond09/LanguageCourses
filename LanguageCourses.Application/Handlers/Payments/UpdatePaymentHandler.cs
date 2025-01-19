using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Payments;

public sealed class UpdatePaymentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<UpdatePaymentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var returnEntity = await _rep.Payments.GetPaymentAsync(request.Id, request.TrackChanges);

        if (returnEntity is null)
            return new PaymentNotFoundResponse(request.Id);

        _mapper.Map(request.Payment, returnEntity);
        await _rep.SaveAsync();

        return new ApiOkResponse<Payment>(returnEntity);
    }
}
