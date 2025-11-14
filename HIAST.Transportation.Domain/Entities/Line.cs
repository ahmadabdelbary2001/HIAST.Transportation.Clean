using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Line : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }

    // Foreign Key
    public int? SupervisorId { get; set; }
    
    // Navigation Properties
    public Supervisor? Supervisor { get; set; }
    public ICollection<LineStop> LineStops { get; set; } = new List<LineStop>();
    public ICollection<LineSubscription> Subscriptions { get; set; } = new List<LineSubscription>();
    public ICollection<Trip> Trips { get; set; } = new List<Trip>();
}