using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Trip;

public class TripDto
{
    public int Id { get; set; }
    public int LineId { get; set; }
    public int BusId { get; set; }
    public int DriverId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public TripStatus Status { get; set; }
    public string LineName { get; set; } = string.Empty;
    public string BusLicensePlate { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
}