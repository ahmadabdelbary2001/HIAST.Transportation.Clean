using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Domain.Entities;

public class Trip : AuditableEntity
{
    public DateTime ScheduledTime { get; set; }
    public DateTime? ActualStartTime { get; set; }
    public TripStatus Status { get; set; } = TripStatus.Scheduled;

    // Foreign Keys
    public int LineId { get; set; }
    public int BusId { get; set; }
    public int DriverId { get; set; }

    // Navigation Properties
    public Line Line { get; set; } = null!;
    public Bus Bus { get; set; } = null!;
    public Driver Driver { get; set; } = null!;
}