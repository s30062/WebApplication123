using Microsoft.AspNetCore.Mvc;
using WebApplication1.API.Models.DTOs;

namespace WebApplication1.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly IDeviceService _service;

    public DevicesController(IDeviceService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<IEnumerable<DeviceDto>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public ActionResult<DeviceDto> GetById(int id)
    {
        var device = _service.GetById(id);
        return device == null ? NotFound("Device not found") : Ok(device);
    }
}