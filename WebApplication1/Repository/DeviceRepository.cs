using DeviceManager.Entities;
using DeviceManager.Logic;
using Microsoft.Data.SqlClient;

namespace DeviceManager.Repository;

public class DeviceRepository : IDeviceRepository
{
    private readonly string _connectionString;

    public DeviceRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<Device> GetAllDevices()
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM Devices", connection);
        connection.Open();
        using var reader = command.ExecuteReader();
        var devices = new List<Device>();

        while (reader.Read())
        {
            devices.Add(DeviceFactory.CreateFromDatabase(reader));
        }
        return devices;
    }

    public Device? GetDeviceById(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("SELECT * FROM Devices WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        using var reader = command.ExecuteReader();
        return reader.Read() ? DeviceFactory.CreateFromDatabase(reader) : null;
    }

    public bool AddDevice(Device device)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("AddDevice", connection) { CommandType = System.Data.CommandType.StoredProcedure };
        DeviceFactory.FillCommandParameters(command, device);
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    public bool UpdateDevice(Device device)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("UpdateDevice", connection) { CommandType = System.Data.CommandType.StoredProcedure };
        DeviceFactory.FillCommandParameters(command, device);
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }

    public bool DeleteDevice(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var command = new SqlCommand("DELETE FROM Devices WHERE Id = @Id", connection);
        command.Parameters.AddWithValue("@Id", id);
        connection.Open();
        return command.ExecuteNonQuery() > 0;
    }
}
