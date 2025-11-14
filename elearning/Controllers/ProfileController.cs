using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTOs.Dtos;
using Services;
using System.Security.Claims;

namespace elearning.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _service;
        public ProfileController(IProfileService service)
        {
            _service = service;
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetProfileById(int id)
        {
            var profile = await _service.GetByIdAsync(id);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpGet("{userId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetProfileByUserId(Guid userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            if (userId != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var profile = await _service.GetByUserIdAsync(userId);
            if (profile == null) return NotFound();
            return Ok(profile);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProfilesPaged([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100) return BadRequest("Invalid pagination parameters.");
            var (items, total) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new { Items = items, Total = total, Page = page, PageSize = pageSize });
        }

        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _service.GetAllAsync();
            return Ok(profiles);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            try
            {
                var profile = await _service.CreateAsync(dto, userId);
                return CreatedAtAction(nameof(GetProfileByUserId), new { userId = userId }, profile);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto, int id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            try
            {
                var profile = await _service.UpdateAsync(id, dto, userId);
                return Ok(profile);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteProfile(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            try
            {
                await _service.DeleteAsync(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
