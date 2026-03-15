namespace Tank_Wiki.Models;

public class Tank
{
    public int Id { get; set; }

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

    public List<TankOfficer> Officers { get; set; } = new();
    public List<General> Generals { get; set; } = new();
}
