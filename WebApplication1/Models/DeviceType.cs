namespace WebApplication1.Api.Models;

public class DeviceType
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Device> Devices { get; set; } = [];
}