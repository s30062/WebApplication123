using DeviceManager.Entities;
using DeviceManager.Repository;

namespace DeviceManager.Logic;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _repository;

    public DeviceService(IDeviceRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<Device> GetAllDevices() => _repository.GetAllDevices();

    public Device? GetDeviceById(int id) => _repository.GetDeviceById(id);

    public bool AddDevice(Device device) => _repository.AddDevice(device);

    public bool UpdateDevice(Device device) => _repository.UpdateDevice(device);

    public bool DeleteDevice(int id) => _repository.DeleteDevice(id);
}