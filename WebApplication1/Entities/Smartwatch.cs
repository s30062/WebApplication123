namespace DeviceManager.Entities;

public class Smartwatch : Device
{
    private int batteryPercentage;

    public int BatteryPercentage
    {
        get => batteryPercentage;
        set
        {
            if (value < 0 || value > 100)
                throw new ArgumentOutOfRangeException(nameof(BatteryPercentage), "Battery percentage must be between 0 and 100.");
            batteryPercentage = value;
        }
    }

    public override string GetShortInfo()
    {
        return $"Smartwatch [ID: {Id}, Name: {Name}]";
    }

    public override string GetFullInfo()
    {
        return $"Smartwatch [ID: {Id}, Name: {Name}, Battery: {BatteryPercentage}%, On: {IsTurnedOn}]";
    }
}