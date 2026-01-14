using HIAST.Transportation.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace HIAST.Transportation.Identity.Models;

public class ApplicationUser : IdentityUser
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? EmployeeNumber { get; set; }
    public Department? Department { get; set; }
}