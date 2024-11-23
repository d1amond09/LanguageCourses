using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record UpdatePaymentCommand(Guid Id, PaymentForUpdateDto Payment, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

