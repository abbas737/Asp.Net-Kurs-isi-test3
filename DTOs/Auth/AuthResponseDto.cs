namespace Tank_Wiki.DTOs.Auth;

public class AuthResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public DateTime ExpireDate { get; set; }
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime RefreshTokenExpireDate { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public List<string> Roles { get; set; } = new List<string>();
}
