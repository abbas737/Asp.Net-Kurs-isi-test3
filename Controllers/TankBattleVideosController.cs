using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tank_Wiki.Common;
using Tank_Wiki.Data;
using Tank_Wiki.DTOs.TankVideo;
using Tank_Wiki.Service.Interfaces;

namespace Tank_Wiki.Controllers;

[Route("api/[controller]")]
[ApiController]


public class TankBattleVideosController : ControllerBase
{
    private readonly ITankBattleVideoService _service;
   public TankBattleVideosController(ITankBattleVideoService service)
    {
        _service = service;
    }



    [HttpGet("battle-video")]
    public async Task<ActionResult<ApiResponse<TankBattleVideoDto>>> GetBattleVideo(int t1, int t2)
    {
        var video = await _service.GetBattleVideoAsync(t1, t2);

        if (video == null)
            return NotFound(ApiResponse<TankBattleVideoDto>.successResponse(null, "Battle video not found"));

        return Ok(ApiResponse<TankBattleVideoDto>.successResponse(video, "Battle video retrieved successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("battle-video")]
    public async Task<ActionResult<ApiResponse<TankBattleVideoDto>>> Create(CreateBattleVideoDto dto)
    {
        var created = await _service.CreateBattleVideoAsync(dto);

        return CreatedAtAction(
            nameof(GetBattleVideo),
            new { t1 = created?.Tank1Id, t2 = created?.Tank2Id },
            ApiResponse<TankBattleVideoDto>.successResponse(created, "Battle video created successfully")
        );
    }
}
