using Tank_Wiki.DTOs.Auth;

namespace Tank_Wiki.Service.Interfaces;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
    Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto);
}
