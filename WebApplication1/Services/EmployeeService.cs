using Microsoft.EntityFrameworkCore;
using WebApplication1.Api.Data;
using WebApplication1.Api.Models;
using WebApplication1.API.Models.DTOs;

public class EmployeeService : IEmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<EmployeeDto> GetAll()
    {
        return _context.Employees
            .Include(e => e.Person)
            .Include(e => e.Position)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = $"{e.Person!.FirstName} {e.Person.LastName}",
                Position = e.Position!.Name,
                Salary = e.Salary,
                HireDate = e.HireDate
            }).ToList();
    }

    public EmployeeDto? GetById(int id)
    {
        return _context.Employees
            .Include(e => e.Person)
            .Include(e => e.Position)
            .Where(e => e.Id == id)
            .Select(e => new EmployeeDto
            {
                Id = e.Id,
                FullName = $"{e.Person!.FirstName} {e.Person.LastName}",
                Position = e.Position!.Name,
                Salary = e.Salary,
                HireDate = e.HireDate
            }).FirstOrDefault();
    }
}