using Microsoft.AspNetCore.Mvc;
using Tank_Wiki.Service.Interfaces;
using Tank_Wiki.DTOs.Request;
using Tank_Wiki.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Tank_Wiki.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EditRequestController : ControllerBase
{
    private readonly IEditRequestService _service;

    public EditRequestController(IEditRequestService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateEditRequestDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return Unauthorized();

        var result = await _service.CreateRequestAsync(dto, userId);

        return Ok(result);
    }

    [HttpGet]
    public async Task<ActionResult<ApiResponse<PagedResult<EditRequestDto>>>> GetAll(int page = 1, int pageSize = 10)
    {
        var result = await _service.GetAllRequestsAsync(page, pageSize);

        return Ok(ApiResponse<PagedResult<EditRequestDto>>
            .successResponse(result, "Requests fetched successfully"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("approve/{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Approve(int id)
    {
        var success = await _service.ApproveRequestAsync(id);

        if (!success)
            return NotFound(ApiResponse<string>
                .successResponse(null, "Request not found"));


        return Ok(ApiResponse<string>
            .successResponse(null, "Request approved"));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("reject/{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Reject(int id)
    {
        var success = await _service.RejectRequestAsync(id);

        if (!success)
            return NotFound(ApiResponse<string>
                .successResponse(null, "Request not found"));


        return Ok(ApiResponse<string>
            .successResponse(null, "Request rejected"));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
    {
        var success = await _service.DeleteRequestAsync(id);

        if (!success)
            return NotFound(ApiResponse<string>
                .successResponse(null, "Request not found"));

        return Ok(ApiResponse<string>
            .successResponse(null, "Request deleted"));
    }

}