using Contracts;
using LanguageCourses.Application.Commands;
using LanguageCourses.Application.Queries;
using LanguageCourses.Domain.DataTransferObjects;
using LanguageCourses.Domain.Exceptions;
using LanguageCourses.Domain.LinkModels;
using LanguageCourses.Domain.RequestFeatures.ModelParameters;
using LanguageCourses.Domain.RequestFeatures;
using LanguageCourses.WebAPI.ActionFilters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using LanguageCourses.WebAPI.Extensions;
using System.Text.Json;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/payments")]
[ApiExplorerSettings(GroupName = "v1")]
[Consumes("application/json")]
[ApiController]
public class PaymentsController(ISender sender) : ApiControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet(Name = "GetPayments")]
    [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
    public async Task<IActionResult> GetPayments([FromQuery] PaymentParameters paymentParameters)
    {
        var linkParams = new LinkPaymentParameters(paymentParameters, HttpContext);
        var baseResult = await _sender.Send(new GetPaymentsQuery(linkParams, TrackChanges: false));
        if (!baseResult.Success)
            return ProcessError(baseResult);

        var (linkResponse, metaData) = baseResult.GetResult<(LinkResponse, MetaData)>();

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(metaData));

        return linkResponse.HasLinks ?
            Ok(linkResponse.LinkedEntities) :
            Ok(linkResponse.ShapedEntities);
    }

    [HttpGet("{id:guid}", Name = "GetPayment")]
    public async Task<IActionResult> GetPayment(Guid id)
    {
        var baseResult = await _sender.Send(new GetPaymentQuery(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var products = baseResult.GetResult<PaymentDto>();
        return Ok(products);
    }

    [HttpPost(Name = "CreatePayment")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentForCreationDto payment)
    {
        var baseResult = await _sender.Send(new CreatePaymentCommand(payment));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        var createdProduct = baseResult.GetResult<PaymentDto>();

        return CreatedAtRoute("GetPayment", new { id = createdProduct.Id }, createdProduct);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeletePayment(Guid id)
    {
        var baseResult = await _sender.Send(new DeletePaymentCommand(id, TrackChanges: false));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdatePayment(Guid id, [FromBody] PaymentForUpdateDto payment)
    {
        var baseResult = await _sender.Send(new UpdatePaymentCommand(id, payment, TrackChanges: true));

        if (!baseResult.Success)
            return ProcessError(baseResult);

        return NoContent();
    }

}
