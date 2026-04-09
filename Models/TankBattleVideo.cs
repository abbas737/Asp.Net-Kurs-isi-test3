namespace Tank_Wiki.Models;

public class TankBattleVideo
{
    public int Id { get; set; }

    public int Tank1Id { get; set; }
    public Tank Tank1 { get; set; } = null!;

    public int Tank2Id { get; set; }
    public Tank Tank2 { get; set; } = null!;

    public string VideoUrl { get; set; } = string.Empty;
}
