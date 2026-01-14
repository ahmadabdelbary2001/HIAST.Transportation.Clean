using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Models.Identity;

public class Employee
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? EmployeeNumber { get; set; }
    public Department? Department { get; set; }
}