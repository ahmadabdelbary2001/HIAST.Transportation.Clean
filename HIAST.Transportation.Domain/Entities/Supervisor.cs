using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Supervisor : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string? ContactInfo { get; set; }
    public int? EmployeeId { get; set; } // Optional link to an employee record

    // Navigation Properties
    public Employee? Employee { get; set; }
    public ICollection<Line> ManagedLines { get; set; } = new List<Line>();
}