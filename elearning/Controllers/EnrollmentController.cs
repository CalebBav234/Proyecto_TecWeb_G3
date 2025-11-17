using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTOs;
using DTOs.Dtos;
using Services;
using System.Security.Claims;

namespace elearning.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _service;
        public EnrollmentController(IEnrollmentService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPagedEnrollments([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100) return BadRequest("Invalid pagination parameters.");
            var (items, total) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new { Items = items, Total = total, Page = page, PageSize = pageSize });
        }

        [HttpGet("all")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllEnrollments()
        {
            var enrollments = await _service.GetAllAsync();
            return Ok(enrollments);
        }

        [HttpGet("user/{userId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetEnrollmentsByUser(Guid userId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            if (userId != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var enrollments = await _service.GetByUserAsync(userId);
            return Ok(enrollments);
        }

        [HttpGet("course/{courseId:guid}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetEnrollmentsByCourse(Guid courseId)
        {
            var enrollments = await _service.GetByCourseAsync(courseId);
            return Ok(enrollments);
        }

        [HttpGet("user/{userId:guid}/course/{courseId:guid}")]
        [Authorize]
        public async Task<IActionResult> GetEnrollment(Guid userId, Guid courseId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            if (userId != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var enrollment = await _service.GetAsync(userId, courseId);
            if (enrollment == null) return NotFound();
            return Ok(enrollment);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CreateEnrollment([FromBody] CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            if (dto.UserId != currentUserId && !User.IsInRole("Admin"))
                return Forbid();

            var enrollment = await _service.CreateEnrollment(dto);
            return CreatedAtAction(nameof(GetEnrollment), new { userId = enrollment.UserId, courseId = enrollment.CourseId }, enrollment);
        }

        [HttpPut("user/{userId:guid}/course/{courseId:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateEnrollment([FromBody] UpdateEnrollmentDto dto, Guid userId, Guid courseId)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            try
            {
                var enrollment = await _service.UpdateEnrollment(dto, userId, courseId, currentUserId);
                return Ok(enrollment);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete("user/{userId:guid}/course/{courseId:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteEnrollment(Guid userId, Guid courseId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var currentUserId))
                return Unauthorized();

            try
            {
                await _service.DeleteEnrollment(userId, courseId, currentUserId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
