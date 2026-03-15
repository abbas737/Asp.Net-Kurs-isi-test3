namespace Tank_Wiki.DTOs.Tank;

public class TankListDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string Type { get; set; } = null!;
    public string? ImageUrl { get; set; }
}
