using WebApplication1.API.Models;
using WebApplication1.API.Models.DTOs;

public interface IDeviceService
{
    IEnumerable<DeviceDto> GetAll();
    DeviceDto? GetById(int id);
}