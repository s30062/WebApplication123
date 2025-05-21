using WebApplication1.Api.Models;

namespace WebApplication1.Api.Services;

public interface IDeviceService
{
    IEnumerable<Device> GetAll();
    Device? GetById(int id);
    int Create(Device device);
    bool Delete(int id);
}