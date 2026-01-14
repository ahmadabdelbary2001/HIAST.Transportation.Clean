namespace HIAST.Transportation.Application.DTOs.Supervisor;

public class SupervisorLineDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public string EmployeeNumber { get; set; } = string.Empty;
    public string EmployeeName { get; set; } = string.Empty;
    public int LineId { get; set; }
    public string LineName { get; set; } = string.Empty;
}