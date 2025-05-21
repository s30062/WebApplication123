using Microsoft.EntityFrameworkCore;
using WebApplication1.Api.Models;

namespace WebApplication1.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) {}

    public DbSet<Device> Devices => Set<Device>();
    public DbSet<DeviceType> DeviceTypes => Set<DeviceType>();
    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Person> Persons => Set<Person>();
    public DbSet<Position> Positions => Set<Position>();
    public DbSet<DeviceEmployee> DeviceEmployees => Set<DeviceEmployee>();
}