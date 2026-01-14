using System.ComponentModel.DataAnnotations;
using HIAST.Transportation.Domain.Enums;

namespace HIAST.Transportation.Application.Models.Identity;

public class RegistrationRequest
{
    [Required(ErrorMessage = "First name is required")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Username is required")]
    [MinLength(6)]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Employee number is required")]
    public string? EmployeeNumber { get; set; }
    
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; }

    public Department? Department { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords don't match")]
    public string? ConfirmPassword { get; set; }
}