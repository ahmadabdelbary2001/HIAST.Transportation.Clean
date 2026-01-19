using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Identity.Models;
using Microsoft.Extensions.Options;
using Novell.Directory.Ldap;

namespace HIAST.Transportation.Identity.Services;

public class LdapService : ILdapService
{
    private readonly LdapSettings _ldapSettings;

    public LdapService(IOptions<LdapSettings> ldapSettings)
    {
        _ldapSettings = ldapSettings.Value;
    }

    public bool ValidateUser(string username, string password)
    {
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return false;
        }

        try
        {
            using var connection = new Novell.Directory.Ldap.LdapConnection();
            connection.Connect(_ldapSettings.Host, _ldapSettings.Port);
            connection.Bind(Novell.Directory.Ldap.LdapConnection.LdapV3, (string)null, (string)null); // Set version to 3
            // connection.SecureSocketLayer = _ldapSettings.UseSsl; // Uncomment if SSL is needed and supported by the library version

            // Bind with the user's credentials
            // Assuming username is just the uid, we might need to construct the DN
            // However, often we bind with a service account first to search for the user DN, 
            // but here we will try to construct the DN directly if possible or bind with what we have.
            // A common pattern is `uid=username,ou=people,dc=example,dc=com`
            // For HIAST, let's assume we can bind with the DN constructed from BaseDn.
            // Or if the server supports binding with just username (Active Directory often does with domain\user).
            
            // Let's try to search for the user first to get the DN, but that requires a bind.
            // If we don't have a service account, we can try to bind with the provided credentials assuming a DN format.
            // Let's assume a standard DN format for now: uid=username,BaseDn
            
            string userDn = $"uid={username},ou=people,{_ldapSettings.BaseDn}";
            
            // NOTE: If HIAST uses Active Directory, the bind might work with just `username@hiast.edu.sy` or `HIAST\username`.
            // Since we don't know the exact structure, we will try a direct bind with the constructed DN.
            // If this fails, we might need to adjust based on specific HIAST LDAP structure.
            
            connection.Bind(userDn, password);
            return connection.Bound;
        }
        catch (LdapException)
        {
            // Log exception
            return false;
        }
        catch (Exception)
        {
            // Log exception
            return false;
        }
    }

    public LdapUser? GetUser(string username)
    {
        try
        {
            using var connection = new LdapConnection();
            connection.Connect(_ldapSettings.Host, _ldapSettings.Port);
            
            // We need to bind to search. If we don't have a service account, we can't search without the user's password.
            // This is a limitation. 
            // However, usually `ValidateUser` is called first. 
            // If we want to get user details *after* validation, we might need to pass the password or keep the connection open.
            // OR, we assume anonymous search is allowed (unlikely).
            // OR, we have a service account (not provided in requirements).
            
            // For this implementation, since we don't have a service account, 
            // we will assume that we can bind with the user's credentials AGAIN if we had them, 
            // but we don't have the password here.
            
            // WORKAROUND: We will assume we can't search without binding. 
            // But wait, the `ValidateUser` succeeds, so we know the password is correct *at that moment*.
            // If we want to fetch details, we should probably do it *during* validation or require a service account.
            
            // Given the constraints and the request, I will modify the flow slightly:
            // I will add a method that takes the password to fetch the user, OR 
            // I will assume there is a read-only service account or anonymous access.
            // Let's assume anonymous access for search is NOT allowed.
            
            // Let's try to bind anonymously just to check.
            // connection.Bind(null, null); 
            
            // Since I cannot change the interface signature easily without affecting the plan (though I can),
            // I will implement a `GetUserDetails` that takes the password, or better, 
            // I will update `ValidateUser` to return the user details if successful.
            // But the interface is `ValidateUser` (bool) and `GetUser` (LdapUser).
            
            // Let's assume for now we can bind anonymously or we have a service account configured in settings (which we don't).
            // actually, the best approach without a service account is to bind with the user's credentials.
            // So `GetUser` might fail if not bound.
            
            // I will update the implementation to fetch user details *inside* ValidateUser if possible, 
            // but `ValidateUser` returns bool.
            
            // Let's stick to the plan but maybe the `GetUser` will have to be called with credentials?
            // Or, we assume the `AuthService` has the password, so we can pass it.
            
            // I will update `ILdapService` to have `Login` that returns `LdapUser?`. 
            // This is cleaner. `ValidateUser` + `GetUser` is 2 calls.
            // Let's change `ILdapService` to `LdapUser? Login(string username, string password);`
            
            return null; // Placeholder as we can't implement this reliably without password or service account
        }
        catch
        {
            return null;
        }
    }
    
    // Revised approach:
    public LdapUser? Login(string username, string password)
    {
         if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            return null;
        }

        try
        {
            using var connection = new Novell.Directory.Ldap.LdapConnection();
            connection.Connect(_ldapSettings.Host, _ldapSettings.Port);
            connection.Bind(Novell.Directory.Ldap.LdapConnection.LdapV3, (string)null, (string)null); // Set version to 3
            
            string userDn = $"uid={username},ou=people,{_ldapSettings.BaseDn}";
            Console.WriteLine($"Attempting to bind with DN: {userDn}");
            connection.Bind(userDn, password);
            Console.WriteLine($"Bind successful: {connection.Bound}");
            
            if (connection.Bound)
            {
                // Now we are bound, we can search for the user details
                var searchResults = connection.Search(
                    _ldapSettings.BaseDn,
                    Novell.Directory.Ldap.LdapConnection.ScopeSub,
                    $"(uid={username})",
                    new[] { "cn", "sn", "givenName", "mail", "employeeNumber", "departmentNumber" }, // Attributes to fetch
                    false
                );

                if (searchResults.HasMore())
                {
                    var entry = searchResults.Next();
                    return new LdapUser
                    {
                        UserName = username,
                        FirstName = entry.GetAttribute("givenName")?.StringValue ?? "",
                        LastName = entry.GetAttribute("sn")?.StringValue ?? "",
                        Email = entry.GetAttribute("mail")?.StringValue ?? "",
                        EmployeeNumber = entry.GetAttribute("employeeNumber")?.StringValue ?? "",
                        Department = entry.GetAttribute("departmentNumber")?.StringValue ?? ""
                    };
                }
                
                // If search fails but bind succeeds (e.g. no read permissions), return basic user
                return new LdapUser { UserName = username };
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"LDAP Error: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
        return null;
    }
}
