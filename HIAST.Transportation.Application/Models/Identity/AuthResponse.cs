namespace HIAST.Transportation.Application.Models.Identity;

public class AuthResponse
{
    public string? Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? Token { get; set; }
    public string? EmployeeNumber { get; set; }
    public List<string> Roles { get; set; } = new();
}