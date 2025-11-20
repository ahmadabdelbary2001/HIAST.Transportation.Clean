namespace HIAST.Transportation.Application.DTOs.Line;

public class UpdateLineDto
{
    public int Id { get; set; }
    public int SupervisorId { get; set; }
    public int BusId { get; set; }
    public int DriverId { get; set; }
    public string ScheduleType { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}