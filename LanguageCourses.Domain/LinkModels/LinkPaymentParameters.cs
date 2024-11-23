using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using Microsoft.AspNetCore.Http;

namespace LanguageCourses.Domain.LinkModels;

public record LinkPaymentParameters(PaymentParameters PaymentParameters, HttpContext Context);
