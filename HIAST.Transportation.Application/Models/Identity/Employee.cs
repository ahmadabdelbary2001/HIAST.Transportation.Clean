using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Models.Identity;

public class Employee
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? EmployeeNumber { get; set; }
    public Department? Department { get; set; }

    // Subscription Details
    public int? LineSubscriptionId { get; set; }
    public int? SubscribedLineId { get; set; }
    public string? SubscribedLineName { get; set; }
    public bool IsSubscriptionActive { get; set; }
}