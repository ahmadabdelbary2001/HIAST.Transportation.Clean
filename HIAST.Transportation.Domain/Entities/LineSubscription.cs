using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class LineSubscription : AuditableEntity
{
    public int EmployeeId { get; set; }
    public int LineId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Navigation properties
    public Employee Employee { get; set; } = null!;
    public Line Line { get; set; } = null!;
}