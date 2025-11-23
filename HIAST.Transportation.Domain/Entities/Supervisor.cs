using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Supervisor : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string ContactInfo { get; set; } = string.Empty;
    public int? EmployeeId { get; set; }

    public Employee? Employee { get; set; }
    
    // Navigation properties
    public ICollection<Line> Lines { get; set; } = new List<Line>();
}