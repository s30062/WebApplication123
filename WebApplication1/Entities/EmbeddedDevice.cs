using System.Text.RegularExpressions;

namespace DeviceManager.Entities;

public class EmbeddedDevice : Device
{
    private string _ipAddress = "";

    public string IpAddress
    {
        get => _ipAddress;
        set
        {
            if (!Regex.IsMatch(value, @"^\b(?:\d{1,3}\.){3}\d{1,3}\b$"))
                throw new ArgumentException("Invalid IP Address format.");
            _ipAddress = value;
        }
    }

    public required string NetworkName { get; set; }

    public override string GetShortInfo()
    {
        return $"Embedded Device [ID: {Id}, Name: {Name}]";
    }

    public override string GetFullInfo()
    {
        return $"Embedded Device [ID: {Id}, Name: {Name}, IP: {IpAddress}, Network: {NetworkName}, On: {IsTurnedOn}]";
    }
}