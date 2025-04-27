
using DeviceManager.Entities;
using Microsoft.Data.SqlClient;
using DeviceManager.Logic; 

using System.Net;

namespace DeviceManager.Logic
{
    public static class DeviceValidator
    {
        public static bool Validate(Device device, out string errorMessage)
        {
            errorMessage = string.Empty;

            switch (device)
            {
                case PersonalComputer pc:
                    if (string.IsNullOrWhiteSpace(pc.OperatingSystem))
                    {
                        errorMessage = "Operating System must not be empty.";
                        return false;
                    }
                    break;

                case EmbeddedDevice embedded:
                    if (string.IsNullOrWhiteSpace(embedded.NetworkName))
                    {
                        errorMessage = "Network Name must not be empty.";
                        return false;
                    }
                    if (!IPAddress.TryParse(embedded.IpAddress, out _))
                    {
                        errorMessage = "Invalid IP address format.";
                        return false;
                    }
                    break;

                case Smartwatch watch:
                    if (watch.BatteryPercentage < 0 || watch.BatteryPercentage > 100)
                    {
                        errorMessage = "Battery percentage must be between 0 and 100.";
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}
