using WebApplication1.API.Models;
using WebApplication1.API.Models.DTOs;


public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetAll();
    EmployeeDto? GetById(int id);
}