using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.DTOs.Trip;

public class UpdateTripDto
{
    public int Id { get; set; }
    public int LineId { get; set; }
    public int BusId { get; set; }
    public int DriverId { get; set; }
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public TripStatus Status { get; set; }
}