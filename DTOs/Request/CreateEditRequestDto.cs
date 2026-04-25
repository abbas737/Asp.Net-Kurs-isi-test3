using static Tank_Wiki.Models.EditRequest;

namespace Tank_Wiki.DTOs.Request;

public class CreateEditRequestDto
{
    public EntityTypes EntityType { get; set; }
    public int EntityId { get; set; }
    public string OldDescription { get; set; } = string.Empty; 
    public string NewDescription { get; set; } = string.Empty;

    // =========================
    // 🔷 GENERAL FIELDS
    // =========================
    public string? GeneralFullName { get; set; }
    public DateTime? GeneralBirthDate { get; set; }
    public DateTime? GeneralDeathDate { get; set; }
    public string? GeneralBiography { get; set; }
    public string? GeneralDescription { get; set; }
    public string? GeneralImageUrl { get; set; }
    public int? GeneralTankId { get; set; }

    // =========================
    // 🔷 TANK OFFICER FIELDS
    // =========================
    public string? OfficerFullName { get; set; }
    public string? OfficerRank { get; set; }
    public DateTime? OfficerBirthDate { get; set; }
    public DateTime? OfficerDeathDate { get; set; }
    public string? OfficerBiography { get; set; }
    public string? OfficerDescription { get; set; }
    public string? OfficerImageUrl { get; set; }
    public int? OfficerTankId { get; set; }
    public string? OfficerTankName { get; set; }

    // =========================
    // 🔷 TANK FIELDS
    // =========================
    public string? TankName { get; set; }
    public string? TankCountry { get; set; }
    public string? TankType { get; set; }
    public int? TankProductionYear { get; set; }
    public double? TankWeight { get; set; }
    public string? TankMainGun { get; set; }
    public int? TankCrew { get; set; }
    public string? TankDescription { get; set; }
}