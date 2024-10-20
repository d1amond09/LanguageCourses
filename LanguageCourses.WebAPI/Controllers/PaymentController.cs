using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebAPI.Controllers;

[Route("api/payments")]
[ApiController]
public class PaymentController(IServiceManager service) : ControllerBase
{
	private readonly IServiceManager _service = service;

	[HttpGet]
	public async Task<IActionResult> GetPayments()
	{
		var payments = await _service.PaymentService.GetAllPaymentsAsync(trackChanges: false);
		return Ok(payments);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetPayment(Guid id)
	{
		var payment = _service.PaymentService.GetPaymentAsync(id, trackChanges: false)
			?? throw new PaymentNotFoundException(id);
		return Ok(payment);
	}

}
