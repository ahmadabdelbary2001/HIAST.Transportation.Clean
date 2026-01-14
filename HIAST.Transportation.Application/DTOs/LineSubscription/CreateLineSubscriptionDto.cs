namespace HIAST.Transportation.Application.DTOs.LineSubscription;

public class CreateLineSubscriptionDto
{
    public string EmployeeId { get; set; } = string.Empty;
    public int LineId { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
}