namespace DeviceManager.Entities;

public class PersonalComputer : Device
{
    public string? OperatingSystem { get; set; }

    public override string GetShortInfo()
    {
        return $"PC [ID: {Id}, Name: {Name}]";
    }

    public override string GetFullInfo()
    {
        return $"PC [ID: {Id}, Name: {Name}, OS: {OperatingSystem ?? "None"}, On: {IsTurnedOn}]";
    }
}