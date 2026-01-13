using HIAST.Transportation.Domain.Common;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Domain.Entities;

public class Employee : AuditableEntity
{
    public string? UserId { get; set; }
    public string EmployeeNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public Department? Department { get; set; }

    // Navigation properties
    public LineSubscription? Subscription { get; set; } 
}