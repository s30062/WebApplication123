namespace DeviceManager.Entities;

public abstract class Device
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public bool IsTurnedOn { get; set; }

    public abstract string GetShortInfo();
    public abstract string GetFullInfo();
}