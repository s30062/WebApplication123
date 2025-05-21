using WebApplication1.API.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Api.Data;
using WebApplication1.API.Models.DTOs;

public class DeviceService : IDeviceService
{
    private readonly ApplicationDbContext _context;

    public DeviceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<DeviceDto> GetAll()
    {
        return _context.Devices
            .Include(d => d.DeviceType)
            .Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                IsEnabled = d.IsEnabled,
                AdditionalProperties = d.AdditionalProperties,
                DeviceType = d.DeviceType != null ? d.DeviceType.Name : null
            }).ToList();
    }

    public DeviceDto? GetById(int id)
    {
        return _context.Devices
            .Include(d => d.DeviceType)
            .Where(d => d.Id == id)
            .Select(d => new DeviceDto
            {
                Id = d.Id,
                Name = d.Name,
                IsEnabled = d.IsEnabled,
                AdditionalProperties = d.AdditionalProperties,
                DeviceType = d.DeviceType != null ? d.DeviceType.Name : null
            }).FirstOrDefault();
    }
}