namespace HIAST.Transportation.Application.DTOs.LineSubscription;

public class LineSubscriptionDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int LineId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string LineName { get; set; } = string.Empty;
}