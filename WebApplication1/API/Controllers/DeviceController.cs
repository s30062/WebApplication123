using System.Text.Json;
using DeviceManager.Entities;
using DeviceManager.Logic;
using Microsoft.AspNetCore.Mvc;

namespace DeviceManager.Api.Controllers;

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
    public IActionResult GetAll() => Ok(_deviceService.GetAllDevices());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var device = _deviceService.GetDeviceById(id);
        if (device == null) return NotFound("Device not found.");
        return Ok(device);
    }

    [HttpPost]
    [Consumes("application/json", "text/plain")]
    public async Task<IActionResult> AddDevice()
    {
        var contentType = Request.ContentType?.ToLower();
        switch (contentType)
        {
            case "application/json":
            {
                using var reader = new StreamReader(Request.Body);
                var json = await reader.ReadToEndAsync();
                var device = JsonSerializer.Deserialize<Device>(json);
                if (device == null) return BadRequest("Invalid JSON format.");

                if (_deviceService.AddDevice(device))
                    return Created("", device);
                return BadRequest("Failed to add device.");
            }
            case "text/plain":
            {
                using var reader = new StreamReader(Request.Body);
                var rawText = await reader.ReadToEndAsync();
                var parts = rawText.Split(';');
                if (parts.Length < 3) return BadRequest("Invalid text format.");

                Device newDevice = parts[0].ToUpper() switch
                {
                    "SW" => new Smartwatch { Name = parts[1], IsTurnedOn = parts[2] == "1", BatteryPercentage = 100 },
                    "P" => new PersonalComputer { Name = parts[1], IsTurnedOn = parts[2] == "1", OperatingSystem = "Windows" },
                    "ED" => new EmbeddedDevice { Name = parts[1], IsTurnedOn = parts[2] == "1", IpAddress = "192.168.0.1", NetworkName = "TestNet" },
                    _ => throw new ArgumentException("Invalid device type.")
                };

                if (_deviceService.AddDevice(newDevice))
                    return Created("", newDevice);
                return BadRequest("Failed to add device.");
            }
            default:
                return BadRequest("Unsupported Content-Type.");
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateDevice(int id, [FromBody] Device device)
    {
        device.Id = id;
        if (!_deviceService.UpdateDevice(device))
            return Conflict("Update failed due to concurrency.");
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteDevice(int id)
    {
        if (!_deviceService.DeleteDevice(id))
            return NotFound("Device not found.");
        return NoContent();
    }
}
