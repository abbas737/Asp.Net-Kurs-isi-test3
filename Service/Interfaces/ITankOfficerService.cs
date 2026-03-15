using Tank_Wiki.Common;
using Tank_Wiki.DTOs.TankOfficer;
using Tank_Wiki.Models;

namespace Tank_Wiki.Service.Interfaces;

public interface ITankOfficerService
{
    Task<PagedResult<TankOfficerDto>> GetAllTankOfficersAsync(int tankId, int page = 1, int pageSize = 10);
    Task<TankOfficerDto?> GetTankOfficerByIdAsync(int id);
    Task<TankOfficerDto> CreateTankOfficerAsync(TankOfficerCreateDto dto);
    Task<TankOfficerDto?> UpdateTankOfficerAsync(int id, TankOfficerUpdateDto dto);
    Task<bool> DeleteTankOfficerAsync(int id);
}