using Microsoft.AspNetCore.Mvc;
using WebApplication1.Api.Models;
using WebApplication1.Api.Services;

namespace WebApplication1.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DevicesController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_deviceService.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var device = _deviceService.GetById(id);
        return device == null ? NotFound() : Ok(device);
    }

    [HttpPost]
    public IActionResult Create(Device device)
    {
        var id = _deviceService.Create(device);
        return CreatedAtAction(nameof(GetById), new { id }, device);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        return _deviceService.Delete(id) ? NoContent() : NotFound();
    }
}