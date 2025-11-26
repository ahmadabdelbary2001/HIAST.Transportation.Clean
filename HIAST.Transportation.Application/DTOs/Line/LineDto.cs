using HIAST.Transportation.Application.DTOs.LineSubscription;
using HIAST.Transportation.Application.DTOs.Stop;

namespace HIAST.Transportation.Application.DTOs.Line;

public class LineDto
{
    public int Id { get; set; }
    public int SupervisorId { get; set; }
    public string SupervisorName { get; set; } = string.Empty;
    public int BusId { get; set; }
    public string BusLicensePlate { get; set; } = string.Empty;
    public int DriverId { get; set; }
    public string DriverName { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public List<StopDto> Stops { get; set; } = new();
    public List<LineSubscriptionDto> Subscriptions { get; set; } = new();
}