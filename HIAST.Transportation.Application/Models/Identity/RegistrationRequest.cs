using System.ComponentModel.DataAnnotations;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Models.Identity;

public class RegistrationRequest
{
    [Required] 
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    [MinLength(6)]
    public string? UserName { get; set; }

    [Required]
    public string? EmployeeNumber { get; set; }

    public Department? Department { get; set; }

    [Required]
    [MinLength(6)]
    public string? Password { get; set; }
}