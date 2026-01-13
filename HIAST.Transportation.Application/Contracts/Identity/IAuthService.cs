using HIAST.Transportation.Application.Models.Identity;

namespace HIAST.Transportation.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}