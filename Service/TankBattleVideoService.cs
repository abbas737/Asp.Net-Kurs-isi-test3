
using Microsoft.EntityFrameworkCore;
using System;
using Tank_Wiki.Data;
using Tank_Wiki.DTOs.TankVideo;
using Tank_Wiki.Models;
using Tank_Wiki.Service.Interfaces;


namespace Tank_Wiki.Service;

public class TankBattleVideoService : ITankBattleVideoService
{
    private readonly TankDbContext _context;

    public TankBattleVideoService(TankDbContext context)
    {
        _context = context;
    }

    public async Task<TankBattleVideoDto?> GetBattleVideoAsync(int t1, int t2)
    {
        var video = await _context.TankBattleVideos
            .FirstOrDefaultAsync(x =>
                (x.Tank1Id == t1 && x.Tank2Id == t2) ||
                (x.Tank1Id == t2 && x.Tank2Id == t1)
            );

        if (video == null) return null;

        return new TankBattleVideoDto
        {
            Tank1Id = video.Tank1Id,
            Tank2Id = video.Tank2Id,
            VideoUrl = video.VideoUrl
        };
    }

    public async Task<TankBattleVideoDto?> CreateBattleVideoAsync(CreateBattleVideoDto dto)
    {
        var entity = new TankBattleVideo
        {
            Tank1Id = dto.Tank1Id,
            Tank2Id = dto.Tank2Id,
            VideoUrl = dto.VideoUrl
        };

        _context.TankBattleVideos.Add(entity);
        await _context.SaveChangesAsync();

        return new TankBattleVideoDto
        {
            Tank1Id = entity.Tank1Id,
            Tank2Id = entity.Tank2Id,
            VideoUrl = entity.VideoUrl
        };
    }
}
