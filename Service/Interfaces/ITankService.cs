using Tank_Wiki.Common;
using Tank_Wiki.DTOs.Tank;

namespace Tank_Wiki.Service.Interfaces;

public interface ITankService
{ 
    Task<PagedResult<TankListDto>> GetAllTanksAsync(TankFilterDto filter);
    Task<TankDetailDto?> GetTankByIdAsync(int id);
    Task<TankDetailDto> CreateTankAsync(CreateTankDto dto);
    Task<TankDetailDto?> UpdateTankAsync(int id, UpdateTankDto dto);
    Task<bool> DeleteTankAsync(int id);
}
