using Microsoft.EntityFrameworkCore;
using Tank_Wiki.Common;
using Tank_Wiki.Data;
using Tank_Wiki.DTOs.Request;
using Tank_Wiki.Models;
using Tank_Wiki.Service.Interfaces;
using static Tank_Wiki.Models.EditRequest;

namespace Tank_Wiki.Service;

public class EditRequestService : IEditRequestService
{
    private readonly TankDbContext _context;

    public EditRequestService(TankDbContext context)
    {
        _context = context;
    }

    public async Task<EditRequestDto> CreateRequestAsync(CreateEditRequestDto dto, string userId)
    {
        var request = new EditRequest
        {
            UserId = userId,
            EntityType = dto.EntityType,
            EntityId = dto.EntityId,

            // GENERAL
            GeneralFullName = dto.GeneralFullName,
            GeneralBirthDate = dto.GeneralBirthDate,
            GeneralDeathDate = dto.GeneralDeathDate,
            GeneralBiography = dto.GeneralBiography,
            GeneralDescription = dto.GeneralDescription,
            GeneralImageUrl = dto.GeneralImageUrl,
            GeneralTankId = dto.GeneralTankId,

            // TANK
            TankName = dto.TankName,
            TankCountry = dto.TankCountry,
            TankType = dto.TankType,
            TankProductionYear = dto.TankProductionYear,
            TankWeight = dto.TankWeight,
            TankMainGun = dto.TankMainGun,
            TankCrew = dto.TankCrew,
            TankDescription = dto.TankDescription,

            // OFFICER
            OfficerFullName = dto.OfficerFullName,
            OfficerRank = dto.OfficerRank,
            OfficerBirthDate = dto.OfficerBirthDate,
            OfficerDeathDate = dto.OfficerDeathDate,
            OfficerBiography = dto.OfficerBiography,
            OfficerDescription = dto.OfficerDescription,
            OfficerImageUrl = dto.OfficerImageUrl,
            OfficerTankId = dto.OfficerTankId,

            Status = "Pending"
        };

        _context.EditRequests.Add(request);
        await _context.SaveChangesAsync();

        return new EditRequestDto
        {
            Id = request.Id,
            UserId = request.UserId,
            EntityType = request.EntityType,
            EntityId = request.EntityId,
            Status = request.Status,
            CreatedAt = request.CreatedAt
        };
    }

    public async Task<PagedResult<EditRequestDto>> GetAllRequestsAsync(int page, int pageSize)
    {
        var query = _context.EditRequests
            .OrderByDescending(r => r.CreatedAt);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(r => new EditRequestDto
            {
                Id = r.Id,
                UserId = r.UserId,
                EntityType = r.EntityType,
                EntityId = r.EntityId,
                Status = r.Status,
                CreatedAt = r.CreatedAt,
                OldDescription = r.OldDescription,
                NewDescription = r.NewDescription

            })
            .ToListAsync();

        return PagedResult<EditRequestDto>.Create(
            items,
            page,
            pageSize,
            totalCount
        );
    }

    public async Task<bool> ApproveRequestAsync(int id)
    {
        var request = await _context.EditRequests
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request == null)
            return false;

        switch (request.EntityType)
        {
            // =========================
            // GENERAL
            // =========================
            case EntityTypes.General:
                {
                    var general = await _context.Generals
                        .FirstOrDefaultAsync(x => x.Id == request.EntityId);

                    if (general == null)
                        return false;

                    general.FullName = request.GeneralFullName ?? general.FullName;
                    general.BirthDate = request.GeneralBirthDate ?? general.BirthDate;
                    general.DeathDate = request.GeneralDeathDate ?? general.DeathDate;
                    general.Biography = request.GeneralBiography ?? general.Biography;
                    general.Description = request.GeneralDescription ?? general.Description;
                    general.ImageUrl = request.GeneralImageUrl ?? general.ImageUrl;
                    general.TankId = request.GeneralTankId ?? general.TankId;

                    break;
                }

            // =========================
            // TANK
            // =========================
            case EntityTypes.Tank:
                {
                    var tank = await _context.Tanks
                        .FirstOrDefaultAsync(x => x.Id == request.EntityId);

                    if (tank == null)
                        return false;

                    tank.Name = request.TankName ?? tank.Name;
                    tank.Country = request.TankCountry ?? tank.Country;
                    tank.Type = request.TankType ?? tank.Type;
                    tank.ProductionYear = request.TankProductionYear ?? tank.ProductionYear;
                    tank.Weight = request.TankWeight ?? tank.Weight;
                    tank.MainGun = request.TankMainGun ?? tank.MainGun;
                    tank.Crew = request.TankCrew ?? tank.Crew;
                    tank.Description = request.TankDescription ?? tank.Description;

                    break;
                }

            // =========================
            // OFFICER
            // =========================
            case EntityTypes.TankOfficer:
                {
                    var officer = await _context.TankOfficers
                        .FirstOrDefaultAsync(x => x.Id == request.EntityId);

                    if (officer == null)
                        return false;

                    officer.FullName = request.OfficerFullName ?? officer.FullName;
                    officer.Rank = request.OfficerRank ?? officer.Rank;
                    officer.BirthDate = request.OfficerBirthDate ?? officer.BirthDate;
                    officer.DeathDate = request.OfficerDeathDate ?? officer.DeathDate;
                    officer.Biography = request.OfficerBiography ?? officer.Biography;
                    officer.Description = request.OfficerDescription ?? officer.Description;
                    officer.ImageUrl = request.OfficerImageUrl ?? officer.ImageUrl;
                    officer.TankId = request.OfficerTankId ?? officer.TankId;

                    break;
                }

            default:
                return false;
        }

        request.Status = "Approved";
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> RejectRequestAsync(int id)
    {
        var request = await _context.EditRequests.FindAsync(id);
        if (request == null) return false;

        request.Status = "Rejected";
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteRequestAsync(int id)
    {
        var request = await _context.EditRequests
            .FirstOrDefaultAsync(x => x.Id == id);

        if (request == null)
            return false;

        _context.EditRequests.Remove(request);
        await _context.SaveChangesAsync();

        return true;
    }
}
