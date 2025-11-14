using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

// Junction table for the many-to-many relationship between Employee and Line
public class LineSubscription : AuditableEntity
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Foreign Keys
    public int EmployeeId { get; set; }
    public int LineId { get; set; }

    // Navigation Properties
    public Employee Employee { get; set; } = null!;
    public Line Line { get; set; } = null!;
}