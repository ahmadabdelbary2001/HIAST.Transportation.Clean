using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

// Junction table for the many-to-many relationship between Employee and Line
public class LineSubscription : AuditableEntity
{
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Foreign Keys
    public string EmployeeUserId { get; set; } = string.Empty;
    public int LineId { get; set; }

    // Navigation Properties
    // public Employee Employee { get; set; } = null!; // Removed
    public Line Line { get; set; } = null!;
}