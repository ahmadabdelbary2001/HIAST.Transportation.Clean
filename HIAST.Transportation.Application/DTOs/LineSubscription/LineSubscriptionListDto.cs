namespace HIAST.Transportation.Application.DTOs.LineSubscription;

public class LineSubscriptionListDto
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = string.Empty;
    public string LineName { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}