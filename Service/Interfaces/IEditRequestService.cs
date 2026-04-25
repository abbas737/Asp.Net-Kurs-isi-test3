using Tank_Wiki.Common;
using Tank_Wiki.DTOs.Request;


namespace Tank_Wiki.Service.Interfaces;

public interface IEditRequestService
{
    Task<EditRequestDto> CreateRequestAsync(CreateEditRequestDto dto, string userId);

    Task<PagedResult<EditRequestDto>> GetAllRequestsAsync(int page = 1, int pageSize = 10);

    Task<bool> ApproveRequestAsync(int id);

    Task<bool> RejectRequestAsync(int id);

    Task<bool> DeleteRequestAsync(int id);
}
