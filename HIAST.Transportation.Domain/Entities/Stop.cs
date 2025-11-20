using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Stop : AuditableEntity
{
    public int LineId { get; set; }
    public string Address { get; set; } = string.Empty;
    public int SequenceOrder { get; set; }

    // Navigation property
    public Line Line { get; set; } = null!;
}