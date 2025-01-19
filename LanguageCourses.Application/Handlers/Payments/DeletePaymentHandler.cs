using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Domain.Entities;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Handlers.Payments;

public sealed class DeletePaymentHandler(IRepositoryManager rep) : IRequestHandler<DeletePaymentCommand, ApiBaseResponse>
{
    private readonly IRepositoryManager _rep = rep;

    public async Task<ApiBaseResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = await _rep.Payments.GetPaymentAsync(request.Id, request.TrackChanges);

        if (payment is null)
            return new PaymentNotFoundResponse(request.Id);

        _rep.Payments.DeletePayment(payment);
        await _rep.SaveAsync();

        return new ApiOkResponse<Payment>(payment);
    }
}
