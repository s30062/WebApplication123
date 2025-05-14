using System.Text.RegularExpressions;



namespace DeviceManager.Entities;

public class EmbeddedDevice : Device
{
    public string IpAddress { get; set; } = "";
    public string NetworkName { get; set; } = "";
}
