using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Employee;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public string EmployeeNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Department? Department { get; set; }
}