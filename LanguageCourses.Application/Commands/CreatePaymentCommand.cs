using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Commands;

public sealed record CreatePaymentCommand(PaymentForCreationDto Payment) :
    IRequest<ApiBaseResponse>;
