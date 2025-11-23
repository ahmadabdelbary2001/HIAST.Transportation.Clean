using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Employee : AuditableEntity
{
    public string EmployeeNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Department { get; set; }
    public bool IsActive { get; set; } = true;

    // Navigation properties
    public ICollection<LineSubscription> LineSubscriptions { get; set; } = new List<LineSubscription>();
}