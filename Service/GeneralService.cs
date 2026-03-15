using Microsoft.EntityFrameworkCore;
using Tank_Wiki.Data;
using Tank_Wiki.Models;
using Tank_Wiki.Service.Interfaces;
using Tank_Wiki.Common;
using Tank_Wiki.DTOs.General;

namespace Tank_Wiki.Service
{
    public class GeneralService : IGeneralService
    {
        private readonly TankDbContext _context;

        public GeneralService(TankDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<GeneralDto>> GetAllGeneralsAsync(int tankId, int page = 1, int pageSize = 10)
        {
            var query = _context.Generals
                                .Where(g => g.TankId == tankId)
                                .AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(g => new GeneralDto
                {
                    Id = g.Id,
                    FullName = g.FullName,
                    BirthDate = g.BirthDate,
                    DeathDate = g.DeathDate,
                    Biography = g.Biography,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    TankId = g.TankId
                })
                .ToListAsync();

            return PagedResult<GeneralDto>.Create(items, page, pageSize, totalCount);
        }

        public async Task<GeneralDto?> GetGeneralByIdAsync(int id)
        {
            var g = await _context.Generals.FindAsync(id);
            if (g == null) return null;

            return new GeneralDto
            {
                Id = g.Id,
                FullName = g.FullName,
                Biography = g.Biography,
                Description = g.Description,
                ImageUrl = g.ImageUrl,
                TankId = g.TankId,
                BirthDate = g.BirthDate,
                DeathDate = g.DeathDate
            };
        }

        public async Task<GeneralDto> CreateGeneralAsync(GeneralCreateDto dto)
        {
            var general = new General
            {
                FullName = dto.FullName,
                BirthDate = dto.BirthDate,
                DeathDate = dto.DeathDate,
                Biography = dto.Biography,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                TankId = dto.TankId
            };

            _context.Generals.Add(general);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Burada xətanın nədən olduğunu görmək üçün log-layırıq
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }

            return new GeneralDto
            {
                Id = general.Id,
                FullName = general.FullName,
                BirthDate = general.BirthDate,
                DeathDate = general.DeathDate,
                Biography = general.Biography,
                Description = general.Description,
                ImageUrl = general.ImageUrl,
                TankId = general.TankId
            };
        }

        public async Task<GeneralDto?> UpdateGeneralAsync(int id, GeneralUpdateDto dto)
        {
            var general = await _context.Generals.FindAsync(id);
            if (general == null) return null;

            general.FullName = dto.FullName;
            general.BirthDate = dto.BirthDate;
            general.DeathDate = dto.DeathDate;
            general.Biography = dto.Biography;
            general.Description = dto.Description;
            general.ImageUrl = dto.ImageUrl;
            general.TankId = dto.TankId;

            await _context.SaveChangesAsync();

            return new GeneralDto
            {
                Id = general.Id,
                FullName = general.FullName,
                BirthDate = general.BirthDate,
                DeathDate = general.DeathDate,
                Biography = general.Biography,
                Description = general.Description,
                ImageUrl = general.ImageUrl,
                TankId = general.TankId
            };
        }

        public async Task<bool> DeleteGeneralAsync(int id)
        {
            var general = await _context.Generals.FindAsync(id);
            if (general == null) return false;

            _context.Generals.Remove(general);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}