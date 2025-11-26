using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Domain.Entities;

public class Line : AuditableEntity
{
    public int SupervisorId { get; set; }
    public int BusId { get; set; }
    public int DriverId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Navigation properties
    public Employee Supervisor { get; set; } = null!;
    public Bus Bus { get; set; } = null!;
    public Driver Driver { get; set; } = null!;
    public ICollection<Stop> Stops { get; set; } = new List<Stop>();
    public ICollection<LineSubscription> LineSubscriptions { get; set; } = new List<LineSubscription>();
}