﻿namespace WebApplication1.Api.Models;

public class Position
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int MinExpYears { get; set; }

    public ICollection<Employee> Employees { get; set; } = [];
}