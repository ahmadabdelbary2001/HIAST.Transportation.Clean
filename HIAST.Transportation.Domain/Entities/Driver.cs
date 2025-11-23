using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Driver : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public DateTime LicenseExpiryDate { get; set; }
    public string? ContactInfo { get; set; }

    // Navigation properties
    public ICollection<Line> Lines { get; set; } = new List<Line>();
}