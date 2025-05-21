using WebApplication1.Api.Data;
using WebApplication1.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Api.Services;

public class DeviceService : IDeviceService
{
    private readonly ApplicationDbContext _context;

    public DeviceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Device> GetAll()
    {
        return _context.Devices.Include(d => d.DeviceType).ToList();
    }

    public Device? GetById(int id)
    {
        return _context.Devices.Include(d => d.DeviceType).FirstOrDefault(d => d.Id == id);
    }

    public int Create(Device device)
    {
        _context.Devices.Add(device);
        _context.SaveChanges();
        return device.Id;
    }

    public bool Delete(int id)
    {
        var device = _context.Devices.FirstOrDefault(d => d.Id == id);
        if (device == null) return false;

        _context.Devices.Remove(device);
        _context.SaveChanges();
        return true;
    }
}