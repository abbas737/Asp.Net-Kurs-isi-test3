namespace Tank_Wiki.Models;

public class RefreshToken
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; } = false;
    public string UserId { get; set; } = string.Empty;
    public AppUser User { get; set; } = null!;

    public bool IsActive => !IsRevoked && DateTime.UtcNow <= ExpiresAt;
}
