namespace HIAST.Transportation.Application.DTOs.LineSubscription;

public class CreateLineSubscriptionDto
{
    public int EmployeeId { get; set; }
    public int LineId { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
}