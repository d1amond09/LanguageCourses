using AutoMapper;
using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Payments;

public sealed class CreatePaymentHandler(IRepositoryManager rep, IMapper mapper) : IRequestHandler<CreatePaymentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;
    private readonly IMapper _mapper = mapper;

    public async Task<ApiBaseResponse> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entityToCreate = _mapper.Map<Payment>(request.Payment);

        _rep.Payments.CreatePayment(entityToCreate);
        await _rep.SaveAsync();

        var entityToReturn = _mapper.Map<PaymentDto>(entityToCreate);
        return new ApiOkResponse<PaymentDto>(entityToReturn);
    }
}
