using System.ComponentModel.DataAnnotations;

namespace HIAST.Transportation.Application.Models.Identity;

public class AuthRequest
{
    [Required(ErrorMessage = "Email or username is required")]
    public string? EmailOrUserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }

    [Display(Name = "Remember Me")]
    public bool RememberMe { get; set; }
}