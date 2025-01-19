using LanguageCourses.Domain.Responses;
using MediatR;

namespace LanguageCourses.Application.Queries;

public sealed record GetPaymentQuery(Guid PaymentId, bool TrackChanges) :
    IRequest<ApiBaseResponse>;

