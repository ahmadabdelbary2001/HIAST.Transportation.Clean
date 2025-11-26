using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Domain.Entities;

public class Stop : AuditableEntity
{
    public int? LineId { get; set; } 
    public string Address { get; set; } = string.Empty;
    public int SequenceOrder { get; set; }
    public StopType StopType { get; set; } = StopType.Intermediate;

    // Navigation property
    public Line? Line { get; set; }
}