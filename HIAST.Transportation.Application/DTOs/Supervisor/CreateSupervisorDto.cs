namespace HIAST.Transportation.Application.DTOs.Supervisor;

public class CreateSupervisorDto
{
    public string Name { get; set; } = string.Empty;
    public string? ContactInfo { get; set; }
    public int? EmployeeId { get; set; }
}