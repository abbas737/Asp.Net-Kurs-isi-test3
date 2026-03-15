using Microsoft.AspNetCore.Identity;

namespace Tank_Wiki.Models;

public class AppUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<RefreshToken>? RefreshTokens { get; set; }
}
