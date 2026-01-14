using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Models.Identity;

/// <summary>
/// Application-layer representation of a user (Employee).
/// This model separates Business/Application concerns from the Infrastructure Identity system.
/// </summary>
public class Employee
{
    public string? Id { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FullName => $"{FirstName} {LastName}";
    public string? UserName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? EmployeeNumber { get; set; }
    public Department? Department { get; set; }

    // Subscription Details
    public int? LineSubscriptionId { get; set; }
    public int? SubscribedLineId { get; set; }
    public string? SubscribedLineName { get; set; }
    public bool IsSubscriptionActive { get; set; }
}