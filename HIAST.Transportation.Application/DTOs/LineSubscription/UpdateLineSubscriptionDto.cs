namespace HIAST.Transportation.Application.DTOs.LineSubscription;

public class UpdateLineSubscriptionDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public int LineId { get; set; }
    public bool IsActive { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}