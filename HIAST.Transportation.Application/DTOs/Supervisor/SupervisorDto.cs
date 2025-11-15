namespace HIAST.Transportation.Application.DTOs.Supervisor;

public class SupervisorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? ContactInfo { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }
}