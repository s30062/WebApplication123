
using DeviceManager.Entities;

namespace DeviceManager.Repository
{
    public interface IDeviceRepository
    {
        IEnumerable<Device> GetAllDevices();
        Device? GetDeviceById(int id); 
        bool AddDevice(Device device);
        bool UpdateDevice(Device device);
        bool DeleteDevice(int id);
    }
}
