namespace HIAST.Transportation.Application.Contracts.Identity;

public interface ILdapService
{
    LdapUser? Login(string username, string password);
}

public class LdapUser
{
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Department { get; set; } = string.Empty;
    public string EmployeeNumber { get; set; } = string.Empty;
}
