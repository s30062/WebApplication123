using DeviceManager.Entities;
using Microsoft.Data.SqlClient;

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
      public static Device? CreateFromDatabase(SqlDataReader reader, string type)
        {
            int id = reader.GetInt32(reader.GetOrdinal("Id"));
            string name = reader.GetString(reader.GetOrdinal("Name"));
            bool isTurnedOn = reader.GetBoolean(reader.GetOrdinal("IsTurnedOn"));

            switch (type)
            {
                case "Smartwatch":
                    int battery = reader.IsDBNull(reader.GetOrdinal("BatteryPercentage")) 
                        ? 0 
                        : reader.GetInt32(reader.GetOrdinal("BatteryPercentage"));
                    return new Smartwatch
                    {
                        Id = id,
                        Name = name,
                        IsTurnedOn = isTurnedOn,
                        BatteryPercentage = battery
                    };

                case "PersonalComputer":
                    string? os = reader.IsDBNull(reader.GetOrdinal("OperatingSystem"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("OperatingSystem"));
                    return new PersonalComputer
                    {
                        Id = id,
                        Name = name,
                        IsTurnedOn = isTurnedOn,
                        OperatingSystem = os
                    };

                case "EmbeddedDevice":
                    string? ip = reader.IsDBNull(reader.GetOrdinal("IpAddress"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("IpAddress"));
                    string? network = reader.IsDBNull(reader.GetOrdinal("NetworkName"))
                        ? null
                        : reader.GetString(reader.GetOrdinal("NetworkName"));
                    return new EmbeddedDevice
                    {
                        Id = id,
                        Name = name,
                        IsTurnedOn = isTurnedOn,
                        IpAddress = ip,
                        NetworkName = network
                    };

                default:
                    return null;
            }
        }
    }

