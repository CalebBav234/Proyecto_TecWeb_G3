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
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _service;
        public CourseController(ICourseService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetPagedCourses([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1 || pageSize < 1 || pageSize > 100) return BadRequest("Invalid pagination parameters.");
            var (items, total) = await _service.GetPagedAsync(page, pageSize);
            return Ok(new { Items = items, Total = total, Page = page, PageSize = pageSize });
        }

        [HttpGet("all")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _service.GetAllAsync();
            return Ok(courses);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _service.GetByIdAsync(id);
            if (course == null) return NotFound();
            return Ok(course);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var course = await _service.CreateCourse(dto);
            return CreatedAtAction(nameof(GetCourseById), new { id = course.Id }, course);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdateCourse([FromBody] UpdateCourseDto dto, int id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized();
            try
            {
                var course = await _service.UpdateCourse(dto, id, userId);
                return Ok(course);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            try
            {
                await _service.DeleteCourse(id, userId);
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}
