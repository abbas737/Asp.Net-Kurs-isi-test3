using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tank_Wiki.Common;
using Tank_Wiki.DTOs.TankOfficer;
using Tank_Wiki.Service.Interfaces;

namespace Tank_Wiki.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TankOfficerController : ControllerBase
{
    private readonly ITankOfficerService _service;

    public TankOfficerController(ITankOfficerService service)
    {
        _service = service;
    }

    [HttpGet("tank/{tankId}")]
    public async Task<ActionResult<ApiResponse<PagedResult<TankOfficerDto>>>> GetAll(int tankId, int page = 1, int pageSize = 10)
    {
        var result = await _service.GetAllTankOfficersAsync(tankId, page, pageSize);
        return Ok(ApiResponse<PagedResult<TankOfficerDto>>.successResponse(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TankOfficerDto>>> GetById(int id)
    {
        var officer = await _service.GetTankOfficerByIdAsync(id);
        if (officer == null)
            return NotFound(ApiResponse<TankOfficerDto>.successResponse(null, "Officer not found"));

        return Ok(ApiResponse<TankOfficerDto>.successResponse(officer));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<TankOfficerDto>>> Create(TankOfficerCreateDto dto)
    {
        var created = await _service.CreateTankOfficerAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id },
            ApiResponse<TankOfficerDto>.successResponse(created, "Officer created successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<TankOfficerDto>>> Update(int id, TankOfficerUpdateDto dto)
    {
        var updated = await _service.UpdateTankOfficerAsync(id, dto);
        if (updated == null)
            return NotFound(ApiResponse<TankOfficerDto>.successResponse(null, "Officer not found"));

        return Ok(ApiResponse<TankOfficerDto>.successResponse(updated, "Officer updated successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        var deleted = await _service.DeleteTankOfficerAsync(id);
        if (!deleted)
            return NotFound(ApiResponse<string>.successResponse(null, "Officer not found"));

        return Ok(ApiResponse<string>.successResponse(null, "Officer deleted successfully"));
    }
}