using Contracts;
using LanguageCourses.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LanguageCourses.WebMVC.Controllers;

public class EmployeesController(IServiceManager service) : Controller
{
    private readonly IServiceManager _service = service;

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employees = await _service.EmployeeService.GetAllEmployeesAsync(trackChanges: false);
        return View(employees);
    }

    [HttpGet]
    public IActionResult Details(Guid id)
    {
        var employee = _service.EmployeeService.GetEmployeeAsync(id, trackChanges: false)
            ?? throw new EmployeeNotFoundException(id);
        return View(employee);
    }

}
