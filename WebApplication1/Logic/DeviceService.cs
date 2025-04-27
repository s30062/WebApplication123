using DeviceManager.Entities;
using Microsoft.Data.SqlClient;
using DeviceManager.Logic; 


namespace DeviceManager.Logic
{
    public class DeviceService : IDeviceService
    {
        private readonly string _connectionString;

        public DeviceService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Device> GetAllDevices()
        {
            var devices = new List<Device>();
            const string query = "SELECT * FROM Devices";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var type = reader.GetString(reader.GetOrdinal("Type"));
                var device = DeviceFactory.CreateFromDatabase(reader, type);
                if (device != null)
                {
                    devices.Add(device);
                }
            }
            return devices;
        }

        public Device? GetDeviceById(int id)
        {
            const string query = "SELECT * FROM Devices WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var type = reader.GetString(reader.GetOrdinal("Type"));
                return DeviceFactory.CreateFromDatabase(reader, type);
            }
            return null;
        }

        public bool AddDevice(Device device)
        {
            const string query = @"
                INSERT INTO Devices (Name, Type, IsTurnedOn, IpAddress, NetworkName, OperatingSystem, BatteryPercentage)
                VALUES (@Name, @Type, @IsTurnedOn, @IpAddress, @NetworkName, @OperatingSystem, @BatteryPercentage)";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);

            FillDeviceCommandParameters(command, device);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected == 1;
        }

        public bool UpdateDevice(int id, Device device)
        {
            const string query = @"
                UPDATE Devices
                SET Name = @Name, Type = @Type, IsTurnedOn = @IsTurnedOn, 
                    IpAddress = @IpAddress, NetworkName = @NetworkName, 
                    OperatingSystem = @OperatingSystem, BatteryPercentage = @BatteryPercentage
                WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Id", id);
            FillDeviceCommandParameters(command, device);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected == 1;
        }

        public bool DeleteDevice(int id)
        {
            const string query = "DELETE FROM Devices WHERE Id = @Id";

            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);

            int rowsAffected = command.ExecuteNonQuery();
            return rowsAffected == 1;
        }

        private void FillDeviceCommandParameters(SqlCommand command, Device device)
        {
            command.Parameters.AddWithValue("@Name", device.Name);
            command.Parameters.AddWithValue("@Type", device.GetType().Name);
            command.Parameters.AddWithValue("@IsTurnedOn", device.IsTurnedOn);

            if (device is EmbeddedDevice embedded)
            {
                command.Parameters.AddWithValue("@IpAddress", embedded.IpAddress);
                command.Parameters.AddWithValue("@NetworkName", embedded.NetworkName);
                command.Parameters.AddWithValue("@OperatingSystem", DBNull.Value);
                command.Parameters.AddWithValue("@BatteryPercentage", DBNull.Value);
            }
            else if (device is PersonalComputer pc)
            {
                command.Parameters.AddWithValue("@IpAddress", DBNull.Value);
                command.Parameters.AddWithValue("@NetworkName", DBNull.Value);
                command.Parameters.AddWithValue("@OperatingSystem", pc.OperatingSystem ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@BatteryPercentage", DBNull.Value);
            }
            else if (device is Smartwatch watch)
            {
                command.Parameters.AddWithValue("@IpAddress", DBNull.Value);
                command.Parameters.AddWithValue("@NetworkName", DBNull.Value);
                command.Parameters.AddWithValue("@OperatingSystem", DBNull.Value);
                command.Parameters.AddWithValue("@BatteryPercentage", watch.BatteryPercentage);
            }
        }
    }
}
