using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Employee;

public class EmployeeListDto
{
    public int Id { get; set; }
    public string EmployeeNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Department? Department { get; set; }
    public bool IsAssigned { get; set; }
    public bool HasSubscription { get; set; }
}