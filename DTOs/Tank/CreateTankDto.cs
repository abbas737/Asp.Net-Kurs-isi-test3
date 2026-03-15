namespace Tank_Wiki.DTOs.Tank;

public class CreateTankDto
{
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Type { get; set; } = null!;
    public int ProductionYear { get; set; }
    public double Weight { get; set; }
    public string MainGun { get; set; } = null!;
    public int Crew { get; set; }
    public string Description { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public string? VideoUrl { get; set; }
}