namespace HIAST.Transportation.Application.DTOs.LineSubscription;

public class LineSubscriptionDto
{
    public int Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public int LineId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string LineName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}