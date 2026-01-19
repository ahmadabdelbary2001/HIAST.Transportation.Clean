using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.Exceptions;
using HIAST.Transportation.Application.Models.Identity;
using HIAST.Transportation.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace HIAST.Transportation.Identity.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    private readonly ILdapService _ldapService;

    public AuthService(UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        SignInManager<ApplicationUser> signInManager,
        ILdapService ldapService)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _signInManager = signInManager;
        _ldapService = ldapService;
    }

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        // 1. Try LDAP Login
        var ldapUser = _ldapService.Login(request.EmailOrUserName!, request.Password!);
        
        ApplicationUser? user;

        if (ldapUser != null)
        {
            // LDAP Auth Succeeded
            // Check if user exists locally
            user = await _userManager.FindByNameAsync(ldapUser.UserName);
            
            if (user == null)
            {
                // Auto-provision user
                user = new ApplicationUser
                {
                    UserName = ldapUser.UserName,
                    Email = ldapUser.Email,
                    FirstName = ldapUser.FirstName,
                    LastName = ldapUser.LastName,
                    EmployeeNumber = ldapUser.EmployeeNumber,
                    Department = Enum.TryParse<HIAST.Transportation.Domain.Enums.Department>(ldapUser.Department, true, out var dept) ? dept : null,
                    EmailConfirmed = true
                };

                var createResult = await _userManager.CreateAsync(user);
                if (!createResult.Succeeded)
                {
                     throw new BadRequestException($"Failed to create local user for LDAP user '{ldapUser.UserName}'. Errors: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                }
                
                // Assign default role
                await _userManager.AddToRoleAsync(user, "Employee");
            }
            else
            {
                // Update existing user details from LDAP if needed (optional)
                // For now, we just ensure they can log in.
            }
        }
        else
        {
            // Fallback to local DB login
            user = await _userManager.FindByEmailAsync(request.EmailOrUserName!);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(request.EmailOrUserName!);
            }

            if (user == null)
            {
                throw new NotFoundException($"User with {request.EmailOrUserName} not found.", request.EmailOrUserName!);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password!, false);

            if (result.Succeeded == false)
            {
                throw new BadRequestException($"Credentials for '{request.EmailOrUserName}' aren't valid.");
            }
        }

        JwtSecurityToken jwtSecurityToken = await GenerateToken(user, request.RememberMe);
        var roles = await _userManager.GetRolesAsync(user);

        var response = new AuthResponse
        {
            Id = user.Id,
            Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
            Email = user.Email,
            UserName = user.UserName,
            EmployeeNumber = user.EmployeeNumber,
            Roles = roles.ToList()
        };

        return response;
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            UserName = request.UserName,
            EmployeeNumber = request.EmployeeNumber,
            PhoneNumber = request.PhoneNumber,
            Department = request.Department,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "Employee");
            return new RegistrationResponse() { UserId = user.Id };
        }
        else
        {
            StringBuilder str = new StringBuilder();
            foreach (var err in result.Errors)
            {
                str.AppendFormat("•{0}\n", err.Description);
            }

            throw new BadRequestException($"{str}");
        }
    }

    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user, bool rememberMe = false)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleIdClaims = roles.Select(q => new Claim("role", q)).ToList();
        var roleClaims = roles.Select(q => new Claim(ClaimTypes.Role, q)).ToList();

        var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("name", user.UserName),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleIdClaims)
            .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: rememberMe ? DateTime.Now.AddDays(30) : DateTime.Now.AddMinutes((double)_jwtSettings.DurationInMinutes!),
            signingCredentials: signingCredentials);
        return jwtSecurityToken;
    }
}