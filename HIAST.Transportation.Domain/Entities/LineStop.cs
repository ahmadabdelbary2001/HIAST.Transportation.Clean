using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

// Junction table for the many-to-many relationship between Line and Stop
public class LineStop : AuditableEntity
{
    public int SequenceOrder { get; set; }

    // Foreign Keys
    public int LineId { get; set; }
    public int StopId { get; set; }

    // Navigation Properties
    public Line Line { get; set; } = null!;
    public Stop Stop { get; set; } = null!;
}