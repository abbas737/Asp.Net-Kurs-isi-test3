namespace Tank_Wiki.Models;

public class General
{
    public int Id { get; set; }
    public string FullName { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public DateTime? DeathDate { get; set; }
    public string? Biography { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public int TankId { get; set; }
    public Tank Tank { get; set; } = null!;
}
