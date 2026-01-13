using HIAST.Transportation.Application.Contracts.Identity;
using HIAST.Transportation.Application.Models.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HIAST.Transportation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(AuthRequest request)
    {
        return Ok(await _authenticationService.Login(request));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        return Ok(await _authenticationService.Register(request));
    }
}
