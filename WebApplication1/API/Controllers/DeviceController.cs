using Microsoft.AspNetCore.Mvc;
using DeviceManager.Entities;
using DeviceManager.Logic;

namespace DeviceManager.Api.Controllers
{
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
        public ActionResult<IEnumerable<Device>> GetAll()
        {
            var devices = _deviceService.GetAllDevices();
            return Ok(devices);
        }

        [HttpGet("{id}")]
        public ActionResult<Device> GetById(int id)
        {
            var device = _deviceService.GetDeviceById(id);
            if (device == null)
                return NotFound("Device not found.");

            return Ok(device);
        }

        [HttpPost]
        public ActionResult AddDevice([FromBody] Device device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!DeviceValidator.Validate(device, out var errorMessage))
                return BadRequest(errorMessage);

            bool created = _deviceService.AddDevice(device);
            if (created)
                return CreatedAtAction(nameof(GetById), new { id = device.Id }, device);

            return StatusCode(500, "Failed to create device.");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateDevice(int id, [FromBody] Device device)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!DeviceValidator.Validate(device, out var errorMessage))
                return BadRequest(errorMessage);

            bool updated = _deviceService.UpdateDevice(id, device);
            if (!updated)
                return NotFound("Device not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteDevice(int id)
        {
            bool deleted = _deviceService.DeleteDevice(id);
            if (!deleted)
                return NotFound("Device not found.");

            return NoContent();
        }

        [HttpPost("import")]
        public ActionResult ImportPlainText([FromBody] string plainText)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                return BadRequest("Input text is empty.");

            try
            {
                var parts = plainText.Split(';');
                if (parts.Length < 3)
                    return BadRequest("Not enough parameters.");

                var type = parts[0];
                var name = parts[1];
                var isTurnedOn = parts[2] == "1";
                var additionalParams = parts.Skip(3).ToArray();

                var device = DeviceFactory.CreateDevice(type, 0, name, isTurnedOn, additionalParams);
                if (device == null)
                    return BadRequest("Invalid device type or parameters.");

                if (!DeviceValidator.Validate(device, out var errorMessage))
                    return BadRequest(errorMessage);

                bool created = _deviceService.AddDevice(device);
                if (created)
                    return Created("", device);

                return StatusCode(500, "Failed to create device.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
