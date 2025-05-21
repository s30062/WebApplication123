namespace WebApplication1.API.Models.DTOs;

public class DeviceDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public bool IsEnabled { get; set; }
    public string AdditionalProperties { get; set; } = null!;
    public string? DeviceType { get; set; }
}