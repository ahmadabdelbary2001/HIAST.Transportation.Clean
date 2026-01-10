using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Driver : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string LicenseNumber { get; set; } = string.Empty;
    public string? ContactInfo { get; set; }

    // Navigation properties
}