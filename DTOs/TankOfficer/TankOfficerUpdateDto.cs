namespace Tank_Wiki.DTOs.TankOfficer;

public class TankOfficerUpdateDto
{
    public string FullName { get; set; } = null!;
    public string Rank { get; set; } = null!;         
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? Biography { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int TankId { get; set; }
}
