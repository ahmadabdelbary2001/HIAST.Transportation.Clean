using HIAST.Transportation.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HIAST.Transportation.Identity.Models;

/// <summary>
/// Infrastructure-layer Identity entity. 
/// Inherits from IdentityUser to integrate with ASP.NET Core Identity.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmployeeNumber { get; set; }
    public Department? Department { get; set; }
}