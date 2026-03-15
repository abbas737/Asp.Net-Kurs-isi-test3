using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tank_Wiki.Service.Interfaces;
using Tank_Wiki.Common;
using Tank_Wiki.DTOs.General;

namespace Tank_Wiki.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GeneralController : ControllerBase
    {
        private readonly IGeneralService _service;

        public GeneralController(IGeneralService service)
        {
            _service = service;
        }

        [HttpGet("tank/{tankId}")]
        public async Task<ActionResult<ApiResponse<PagedResult<GeneralDto>>>> GetAll(int tankId, int page = 1, int pageSize = 10)
        {
            var result = await _service.GetAllGeneralsAsync(tankId, page, pageSize);
            return Ok(ApiResponse<PagedResult<GeneralDto>>.successResponse(result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<GeneralDto>>> GetById(int id)
        {
            var general = await _service.GetGeneralByIdAsync(id);
            if (general == null)
                return NotFound(ApiResponse<GeneralDto>.successResponse(null, "General not found"));

            return Ok(ApiResponse<GeneralDto>.successResponse(general, "General retrieved successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse<GeneralDto>>> Create(GeneralCreateDto dto)
        {
            var created = await _service.CreateGeneralAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id },
                ApiResponse<GeneralDto>.successResponse(created, "General created successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ApiResponse<GeneralDto>>> Update(int id, GeneralUpdateDto dto)
        {
            var updated = await _service.UpdateGeneralAsync(id, dto);
            if (updated == null)
                return NotFound(ApiResponse<GeneralDto>.successResponse(null, "General not found"));

            return Ok(ApiResponse<GeneralDto>.successResponse(updated, "General updated successfully"));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            var deleted = await _service.DeleteGeneralAsync(id);
            if (!deleted)
                return NotFound(ApiResponse<string>.successResponse(null, "General not found"));

            return Ok(ApiResponse<string>.successResponse(null, "General deleted successfully"));
        }
    }
}