using Contracts;
using Contracts.Services;
using Microsoft.AspNetCore.Http;
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
		var Payments = await _service.PaymentService.GetAllPaymentsAsync(trackChanges: false);
		return Ok(Payments);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetCompany(Guid id)
	{
		var company = _service.PaymentService.GetPaymentAsync(id, trackChanges: false);
		return Ok(company);
	}

}
