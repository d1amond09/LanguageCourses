﻿using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebMVC.Controllers;

public class PaymentsController(IServiceManager service) : Controller
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var payments = await _service.PaymentService.GetAllPaymentsAsync(trackChanges: false);
        return View(payments);
    }

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var payment = _service.PaymentService.GetPaymentAsync(id, trackChanges: false)
            ?? throw new PaymentNotFoundException(id);
        return View(payment);
    }

}
