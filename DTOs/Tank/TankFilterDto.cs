namespace Tank_Wiki.DTOs.Tank;

public class TankFilterDto
{
    public string? Search { get; set; }

    public string? Country { get; set; }

    public string? Type { get; set; }

    public int? Year { get; set; }

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}
