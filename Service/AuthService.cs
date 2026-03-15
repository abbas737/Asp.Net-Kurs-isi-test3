using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tank_Wiki.Data;
using Tank_Wiki.DTOs.Auth;
using Tank_Wiki.Models;
using Tank_Wiki.Service.Interfaces;

namespace Tank_Wiki.Service;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TankDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<AppUser> userManager, TankDbContext context , IConfiguration configuration)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userManager.FindByEmailAsync(dto.Email);
        if (existingUser != null)
            throw new InvalidOperationException("User with this email already exists.");

        var user = new AppUser
        {
            UserName = dto.UserName,
            Email = dto.Email
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(",", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException(errors);
        }

        // Default role
        await _userManager.AddToRoleAsync(user, "User");

        return await GenerateTokenAsync(user);
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) throw new InvalidOperationException("Invalid email or password.");

        var passwordValid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!passwordValid) throw new InvalidOperationException("Invalid email or password.");

        return await GenerateTokenAsync(user);
    }

    private async Task<AuthResponseDto> GenerateTokenAsync(AppUser user)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var secretKey = jwtSettings["SecretKey"];
        var issuer = jwtSettings["Issuer"];
        var audience = jwtSettings["Audience"];
        var expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"]!);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id ?? Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
        new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expirationMinutes),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        // ✅ RefreshToken yarat
        var refreshToken = new RefreshToken
        {
            Token = Guid.NewGuid().ToString(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            UserId = user.Id
        };

        _context.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();

        return new AuthResponseDto
        {
            AccessToken = tokenString,
            ExpireDate = token.ValidTo,
            RefreshToken = refreshToken.Token,
            RefreshTokenExpireDate = refreshToken.ExpiresAt,
            Email = user.Email ?? string.Empty,
            UserName = user.UserName ?? string.Empty,
            Roles = roles.ToList()
        };
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto)
    {
        var refreshToken = await _context.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == dto.RefreshToken);

        if (refreshToken == null || !refreshToken.IsActive)
            throw new InvalidOperationException("Invalid refresh token.");

        // Token-i passiv et
        refreshToken.IsRevoked = true;
        await _context.SaveChangesAsync();

        return await GenerateTokenAsync(refreshToken.User);
    }
}