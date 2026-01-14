namespace HIAST.Transportation.Application.DTOs.Line;

public class CreateLineDto
{
    public string SupervisorId { get; set; } = string.Empty;
    public int BusId { get; set; }
    public int DriverId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}