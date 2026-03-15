using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tank_Wiki.Service.Interfaces;
using Tank_Wiki.Common;
using Tank_Wiki.DTOs.Tank;

namespace Tank_Wiki.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class TankController : ControllerBase
{
    private readonly ITankService _service;

    public TankController(ITankService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<TankListDto>>>> GetAll([FromQuery] TankFilterDto filter)
    {
        var result = await _service.GetAllTanksAsync(filter);

        return Ok(ApiResponse<PagedResult<TankListDto>>
            .successResponse(result));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TankDetailDto>>> GetById(int id)
    {
        var tank = await _service.GetTankByIdAsync(id);

        if (tank == null)
            return NotFound(ApiResponse<TankDetailDto>.successResponse(
                null, "Tank not found"
            ));

        return Ok(ApiResponse<TankDetailDto>.successResponse(
            tank, "Tank retrieved successfully"
        ));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult<ApiResponse<TankDetailDto>>> Create(CreateTankDto dto)
    {
        var createdTank = await _service.CreateTankAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = createdTank.Id },
            ApiResponse<TankDetailDto>.successResponse(createdTank, "Tank created successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<TankDetailDto>>> Update(int id, UpdateTankDto dto)
    {
        var updatedTank = await _service.UpdateTankAsync(id, dto);

        if (updatedTank == null)
            return NotFound(ApiResponse<TankDetailDto>.successResponse(
                null, "Tank not found"
            ));

        return Ok(ApiResponse<TankDetailDto>.successResponse(
            updatedTank, "Tank updated successfully"
        ));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        var deleted = await _service.DeleteTankAsync(id);

        if (!deleted)
            return NotFound(ApiResponse<string>.successResponse(
                null, "Tank not found"
            ));

        return Ok(ApiResponse<string>.successResponse(
            null, "Tank deleted successfully"
        ));
    }
}