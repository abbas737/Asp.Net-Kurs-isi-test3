namespace Tank_Wiki.DTOs.TankOfficer;

public class TankOfficerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Rank { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? Biography { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int TankId { get; set; }
    public string TankName { get; set; } = null!;


    public int Age => DeathDate.HasValue
        ? DeathDate.Value.Year - BirthDate.Year
        : DateTime.UtcNow.Year - BirthDate.Year;
}