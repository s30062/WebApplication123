using DeviceManager.Entities;
using Microsoft.Data.SqlClient;

namespace DeviceManager.Logic;

public static class DeviceFactory
{
    public static Device CreateFromDatabase(SqlDataReader reader)
    {
        var type = reader["Type"].ToString();
        return type switch
        {
            "Smartwatch" => new Smartwatch { Name = reader["Name"].ToString(), IsTurnedOn = (bool)reader["IsTurnedOn"], BatteryPercentage = (int)reader["BatteryPercentage"] },
            "PersonalComputer" => new PersonalComputer { Name = reader["Name"].ToString(), IsTurnedOn = (bool)reader["IsTurnedOn"], OperatingSystem = reader["OperatingSystem"].ToString() },
            "EmbeddedDevice" => new EmbeddedDevice { Name = reader["Name"].ToString(), IsTurnedOn = (bool)reader["IsTurnedOn"], IpAddress = reader["IpAddress"].ToString(), NetworkName = reader["NetworkName"].ToString() },
            _ => throw new InvalidOperationException("Unknown device type.")
        };
    }

    public static void FillCommandParameters(SqlCommand command, Device device)
    {
        command.Parameters.AddWithValue("@Name", device.Name);
        command.Parameters.AddWithValue("@IsTurnedOn", device.IsTurnedOn);
    }
}