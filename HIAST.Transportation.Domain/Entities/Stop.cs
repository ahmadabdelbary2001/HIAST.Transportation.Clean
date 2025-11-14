using HIAST.Transportation.Domain.Common;

namespace HIAST.Transportation.Domain.Entities;

public class Stop : AuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? Address { get; set; }

    // Navigation Properties
    public ICollection<LineStop> LineStops { get; set; } = new List<LineStop>();
}