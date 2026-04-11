using Microsoft.EntityFrameworkCore;
using Tank_Wiki.Data;
using Tank_Wiki.Models;
using Tank_Wiki.Service.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Tank_Wiki.Common;
using Tank_Wiki.DTOs.Tank;
using Tank_Wiki.DTOs.General;
using Tank_Wiki.DTOs.TankOfficer;

namespace Tank_Wiki.Service;

public class TankService : ITankService
{
    private readonly TankDbContext _context;
    private readonly IMapper _mapper;

    public TankService(TankDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<TankListDto>> GetAllTanksAsync(TankFilterDto filter)
    {
        var query = _context.Tanks.AsQueryable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
            query = query.Where(t => t.Name.Contains(filter.Search));

        if (!string.IsNullOrWhiteSpace(filter.Country))
            query = query.Where(t => t.Country == filter.Country);

        if (!string.IsNullOrWhiteSpace(filter.Type))
            query = query.Where(t => t.Type == filter.Type);

        if (filter.Year.HasValue)
            query = query.Where(t => t.ProductionYear == filter.Year.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(t => new TankListDto
            {
                Id = t.Id,
                Name = t.Name,
                Country = t.Country,
                Type = t.Type,
                ImageUrl = t.ImageUrl
            })
            .ToListAsync();

        return PagedResult<TankListDto>.Create(items, filter.Page, filter.PageSize, totalCount);
    }


    public async Task<TankDetailDto?> GetTankByIdAsync(int id)
    {
        var tank = await _context.Tanks
            .Include(t => t.Officers)
            .Include(t => t.Generals)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tank == null) return null;

        return new TankDetailDto
        {
            Id = tank.Id,
            Name = tank.Name,
            Country = tank.Country,
            Type = tank.Type,
            ProductionYear = tank.ProductionYear,
            Weight = tank.Weight,
            MainGun = tank.MainGun,
            Crew = tank.Crew,
            Description = tank.Description,
            ImageUrl = tank.ImageUrl,
            VideoUrl = tank.VideoUrl,
            Officers = tank.Officers.Select(o => new TankOfficerDto
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
            }).ToList(),
            Generals = tank.Generals.Select(g => new GeneralDto
            {
                Id = g.Id,
                FullName = g.FullName,
                BirthDate = g.BirthDate,
                DeathDate = g.DeathDate,
                Biography = g.Biography,
                Description = g.Description,
                ImageUrl = g.ImageUrl,
                TankId = g.TankId,
            }).ToList()
        };
    }

    public async Task<TankDetailDto> CreateTankAsync(CreateTankDto dto)
    {
        var tank = _mapper.Map<Tank>(dto);
        await _context.Tanks.AddAsync(tank);
        await _context.SaveChangesAsync();

        return _mapper.Map<TankDetailDto>(tank);
    }

    public async Task<TankDetailDto?> UpdateTankAsync(int id, UpdateTankDto dto)
    {
        var tank = await _context.Tanks.FindAsync(id);

        if (tank == null)
            return null;

        _mapper.Map(dto, tank);

        await _context.SaveChangesAsync();

        return _mapper.Map<TankDetailDto>(tank);
    }

    public async Task<bool> DeleteTankAsync(int id)
    {
        var tank = await _context.Tanks
            .Include(t => t.Officers)
            .Include(t => t.Generals)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tank == null) return false;

        var battles = await _context.TankBattleVideos
            .Where(x => x.Tank1Id == id || x.Tank2Id == id)
            .ToListAsync();

        _context.TankBattleVideos.RemoveRange(battles);

        _context.TankOfficers.RemoveRange(tank.Officers);
        _context.Generals.RemoveRange(tank.Generals);

        _context.Tanks.Remove(tank);

        await _context.SaveChangesAsync();

        return true;
    }
}