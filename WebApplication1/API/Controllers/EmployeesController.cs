using Microsoft.AspNetCore.Mvc;
using WebApplication1.API.Models.DTOs;


namespace WebApplication1.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EmployeeDto>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public ActionResult<EmployeeDto> GetById(int id)
    {
        var employee = _service.GetById(id);
        return employee == null ? NotFound("Employee not found") : Ok(employee);
    }
}