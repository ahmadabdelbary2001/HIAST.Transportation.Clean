using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Domain.Entities;

public class Bus : AuditableEntity
{
    public string LicensePlate { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public BusStatus Status { get; set; } = BusStatus.Available;

    // Navigation properties
    public ICollection<Line> Lines { get; set; } = new List<Line>();
}