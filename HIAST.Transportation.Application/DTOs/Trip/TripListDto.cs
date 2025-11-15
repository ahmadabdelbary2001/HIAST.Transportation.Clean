using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Trip;

public class TripListDto
{
    public int Id { get; set; }
    public string LineName { get; set; } = string.Empty;
    public string BusLicensePlate { get; set; } = string.Empty;
    public string DriverName { get; set; } = string.Empty;
    public DateTime ScheduledTime { get; set; }
    public TripStatus Status { get; set; }
}