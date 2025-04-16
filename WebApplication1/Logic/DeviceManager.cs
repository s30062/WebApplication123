using DeviceManager.Entities;

namespace DeviceManager.Logic;

public class DeviceManager
{
    private readonly List<Device> devices = new();
    private int nextId = 1;
    private const int MaxDevices = 15;

    public List<Device> GetAllDevices() => devices;

    public Device? GetDeviceById(int id) =>
        devices.FirstOrDefault(d => d.Id == id);

    public bool AddDevice(Device device)
    {
        if (devices.Count >= MaxDevices) return false;

        device.Id = nextId++;
        devices.Add(device);
        return true;
    }

    public bool UpdateDevice(int id, Device updatedDevice)
    {
        var existing = GetDeviceById(id);
        if (existing == null) return false;

        // Update shared properties
        existing.Name = updatedDevice.Name;
        existing.IsTurnedOn = updatedDevice.IsTurnedOn;

        switch (existing)
        {
            case Smartwatch sw when updatedDevice is Smartwatch newSw:
                sw.BatteryPercentage = newSw.BatteryPercentage;
                break;

            case PersonalComputer pc when updatedDevice is PersonalComputer newPc:
                pc.OperatingSystem = newPc.OperatingSystem;
                break;

            case EmbeddedDevice ed when updatedDevice is EmbeddedDevice newEd:
                ed.IpAddress = newEd.IpAddress;
                ed.NetworkName = newEd.NetworkName;
                break;
        }

        return true;
    }

    public bool DeleteDevice(int id) =>
        devices.RemoveAll(d => d.Id == id) > 0;
}