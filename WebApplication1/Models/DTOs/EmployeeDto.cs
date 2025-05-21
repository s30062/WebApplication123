namespace WebApplication1.API.Models.DTOs;

public class EmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Position { get; set; } = null!;
    public decimal Salary { get; set; }
    public DateTime HireDate { get; set; }
}