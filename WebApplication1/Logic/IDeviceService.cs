using DeviceManager.Entities;

namespace DeviceManager.Logic
{
    public interface IDeviceService
    {
        IEnumerable<Device> GetAllDevices();
        Device? GetDeviceById(int id);
        bool AddDevice(Device device);
        bool UpdateDevice(int id, Device updatedDevice);
        bool DeleteDevice(int id);
    }
}