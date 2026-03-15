using Tank_Wiki.Common;
using Tank_Wiki.DTOs.General;

namespace Tank_Wiki.Service.Interfaces;

public interface IGeneralService
{
    Task<PagedResult<GeneralDto>> GetAllGeneralsAsync(int tankId, int page = 1, int pageSize = 10);
    Task<GeneralDto?> GetGeneralByIdAsync(int id);
    Task<GeneralDto> CreateGeneralAsync(GeneralCreateDto dto);
    Task<GeneralDto?> UpdateGeneralAsync(int id, GeneralUpdateDto dto);
    Task<bool> DeleteGeneralAsync(int id);
}
