namespace HIAST.Transportation.Application.Models.Identity;

public class AuthRequest
{
    public string? EmailOrUserName { get; set; }
    public string? Password { get; set; }
    public bool RememberMe { get; set; }
}