using Microsoft.EntityFrameworkCore;
using Tank_Wiki.Common;
using Tank_Wiki.Data;
using Tank_Wiki.DTOs.TankOfficer;
using Tank_Wiki.Models;
using Tank_Wiki.Service.Interfaces;

namespace Tank_Wiki.Service;

public class TankOfficerService : ITankOfficerService
{
    private readonly TankDbContext _context;

    public TankOfficerService(TankDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<TankOfficerDto>> GetAllTankOfficersAsync(int tankId, int page = 1, int pageSize = 10)
    {
        var query = _context.TankOfficers
            .Include(o => o.Tank)
            .Where(o => o.TankId == tankId);




        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(o => new TankOfficerDto
            {
                Id = o.Id,
                FullName = o.FullName,
                Rank = o.Rank,
                BirthDate = o.BirthDate,
                DeathDate = o.DeathDate,
                Biography = o.Biography,
                Description = o.Description,
                ImageUrl = o.ImageUrl,
                TankId = o.TankId,
                TankName = o.Tank.Name
            })
            .ToListAsync();

        return PagedResult<TankOfficerDto>.Create(items, page, pageSize, totalCount);
    }

    public async Task<TankOfficerDto?> GetTankOfficerByIdAsync(int id)
    {
        var officer = await _context.TankOfficers.FindAsync(id);
        if (officer == null) return null;

        return new TankOfficerDto
        {
            Id = officer.Id,
            FullName = officer.FullName,
            Rank = officer.Rank,
            BirthDate = officer.BirthDate,
            DeathDate = officer.DeathDate,
            Biography = officer.Biography,
            Description = officer.Description,
            ImageUrl = officer.ImageUrl,
            TankId = officer.TankId
        };
    }

    public async Task<TankOfficerDto> CreateTankOfficerAsync(TankOfficerCreateDto dto)
    {
        var officer = new TankOfficer
        {
            FullName = dto.FullName,
            Rank = dto.Rank,
            BirthDate = dto.BirthDate,
            DeathDate = dto.DeathDate,
            Biography = dto.Biography,
            Description = dto.Description,
            ImageUrl = dto.ImageUrl,
            TankId = dto.TankId
        };

        _context.TankOfficers.Add(officer);
        await _context.SaveChangesAsync();

        return new TankOfficerDto
        {
            Id = officer.Id,
            FullName = officer.FullName,
            Rank = officer.Rank,
            BirthDate = officer.BirthDate,
            DeathDate = officer.DeathDate,
            Biography = officer.Biography,
            Description = officer.Description,
            ImageUrl = officer.ImageUrl,
            TankId = officer.TankId
        };
    }

    public async Task<TankOfficerDto?> UpdateTankOfficerAsync(int id, TankOfficerUpdateDto dto)
    {
        var officer = await _context.TankOfficers.FindAsync(id);
        if (officer == null) return null;

        officer.FullName = dto.FullName;
        officer.Rank = dto.Rank;
        officer.BirthDate = dto.BirthDate;
        officer.DeathDate = dto.DeathDate;
        officer.Biography = dto.Biography;
        officer.Description = dto.Description;
        officer.ImageUrl = dto.ImageUrl;
        officer.TankId = dto.TankId;

        await _context.SaveChangesAsync();

        return new TankOfficerDto
        {
            Id = officer.Id,
            FullName = officer.FullName,
            Rank = officer.Rank,
            BirthDate = officer.BirthDate,
            DeathDate = officer.DeathDate,
            Biography = officer.Biography,
            Description = officer.Description,
            ImageUrl = officer.ImageUrl,
            TankId = officer.TankId
        };
    }

    public async Task<bool> DeleteTankOfficerAsync(int id)
    {
        var officer = await _context.TankOfficers.FindAsync(id);
        if (officer == null) return false;

        _context.TankOfficers.Remove(officer);
        await _context.SaveChangesAsync();

        return true;
    }
}