using DeviceManager.Entities;

namespace DeviceManager.Logic;

public static class DeviceFactory
{
    public static Device? CreateDevice(string type, int id, string name, bool isTurnedOn, string[] additionalParams)
    {
        return type.ToUpper() switch
        {
            "SW" when additionalParams.Length >= 1 => new Smartwatch
            {
                Id = id,
                Name = name,
                IsTurnedOn = isTurnedOn,
                BatteryPercentage = int.Parse(additionalParams[0])
            },
            "P" when additionalParams.Length >= 1 => new PersonalComputer
            {
                Id = id,
                Name = name,
                IsTurnedOn = isTurnedOn,
                OperatingSystem = additionalParams[0]
            },
            "ED" when additionalParams.Length >= 2 => new EmbeddedDevice
            {
                Id = id,
                Name = name,
                IsTurnedOn = isTurnedOn,
                IpAddress = additionalParams[0],
                NetworkName = additionalParams[1]
            },
            _ => null
        };
    }
}