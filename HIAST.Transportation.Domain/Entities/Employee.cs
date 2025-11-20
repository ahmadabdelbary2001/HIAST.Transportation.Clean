using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Employee : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string EmployeeId { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;

    // Navigation properties
    public ICollection<LineSubscription> LineSubscriptions { get; set; } = new List<LineSubscription>();
}