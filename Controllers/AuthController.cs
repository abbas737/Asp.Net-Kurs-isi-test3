using Microsoft.AspNetCore.Mvc;
using Tank_Wiki.Common;
using Tank_Wiki.DTOs.Auth;
using Tank_Wiki.Service.Interfaces;

namespace Tank_Wiki.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login(LoginDto dto)
    {
        var authResult = await _authService.LoginAsync(dto);
        return Ok(ApiResponse<AuthResponseDto>.successResponse(authResult, "Login successful"));
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthResponseDto>> RefreshToken(RefreshTokenRequestDto dto)
    {
        var result = await _authService.RefreshTokenAsync(dto);
        return Ok(result);
    }
}
