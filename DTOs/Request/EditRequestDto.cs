using static Tank_Wiki.Models.EditRequest;

namespace Tank_Wiki.DTOs.Request;

public class EditRequestDto
{
    public int Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public EntityTypes EntityType { get; set; }

    public int EntityId { get; set; }

    public string OldDescription { get; set; } = string.Empty;
    public string NewDescription { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }
}
