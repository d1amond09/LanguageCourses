using Contracts;
using Contracts.ModelLinks;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.Models;
using Microsoft.Net.Http.Headers;

namespace LanguageCourses.WebAPI.Utility;

public class PaymentLinks(LinkGenerator linkGenerator, IDataShaper<PaymentDto> dataShaper) : IPaymentLinks
{
    private readonly LinkGenerator _linkGenerator = linkGenerator;
    private readonly IDataShaper<PaymentDto> _dataShaper = dataShaper;

    public LinkResponse TryGenerateLinks(IEnumerable<PaymentDto> paymentsDto, string fields, HttpContext httpContext)
    {
        var shapedPayments = ShapeData(paymentsDto, fields);

        if (ShouldGenerateLinks(httpContext))
            return ReturnLinkdedPayments(paymentsDto, fields, httpContext, shapedPayments);

        return ReturnShapedPayments(shapedPayments);
    }

    private LinkResponse ReturnShapedPayments(List<Entity> shapedPayments) =>
        new() { ShapedEntities = shapedPayments };

    private List<Entity> ShapeData(IEnumerable<PaymentDto> paymentsDto, string fields) =>
        _dataShaper.ShapeData(paymentsDto, fields)
            .Select(e => e.Entity)
            .ToList();

    private bool ShouldGenerateLinks(HttpContext httpContext)
    {
        var mediaType = (MediaTypeHeaderValue?)httpContext.Items["AcceptHeaderMediaType"];
        ArgumentNullException.ThrowIfNull(mediaType);
        return mediaType.SubTypeWithoutSuffix
            .EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase);
    }
    private LinkResponse ReturnLinkdedPayments(IEnumerable<PaymentDto> paymentsDto, string fields, HttpContext httpContext, List<Entity> shapedPayments)
    {
        var paymentDtoList = paymentsDto.ToList();

        for (var index = 0; index < paymentDtoList.Count; index++)
        {
            var paymentLinks = CreateLinksForPayments(httpContext, paymentDtoList[index].Id, fields);
            shapedPayments[index].Add("Links", paymentLinks);
        }

        var paymentCollection = new LinkCollectionWrapper<Entity>(shapedPayments);
        var linkedPayments = CreateLinksForPayments(httpContext, paymentCollection);

        return new LinkResponse { HasLinks = true, LinkedEntities = linkedPayments };
    }

    private List<Link> CreateLinksForPayments(HttpContext httpContext, Guid id, string fields = "")
    {
        List<Link> links = [
            new Link(_linkGenerator.GetUriByAction(httpContext, "GetPayment", values: new { id, fields })!, "self", "GET"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "DeletePayment", values: new { id })!, "delete_payment", "DELETE"),
            new Link(_linkGenerator.GetUriByAction(httpContext, "UpdatePayment", values: new { id })!, "update_payment", "PUT")
        ];

        return links;
    }

    private LinkCollectionWrapper<Entity> CreateLinksForPayments(HttpContext httpContext, LinkCollectionWrapper<Entity> paymentsWrapper)
    {
        paymentsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(httpContext, "GetPayment", values: new { })!, "self", "GET"));
        return paymentsWrapper;
    }
}
