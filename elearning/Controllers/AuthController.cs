using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using DTOs.Dtos;
using Services;
using System.Security.Claims;
using Serilog;

namespace elearning.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService service, ILogger<AuthController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            try
            {
                var id = await _service.RegisterAsync(dto);
                _logger.LogInformation("User registered successfully with ID: {UserId}", id);
                return CreatedAtAction(nameof(Register), new { id }, null);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during user registration for email: {Email}", dto.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            try
            {
                var (ok, response) = await _service.LoginAsync(dto);
                if (!ok || response is null)
                {
                    _logger.LogWarning("Login failed for email: {Email}", dto.Email);
                    return Unauthorized();
                }
                _logger.LogInformation("Login successful for email: {Email}", dto.Email);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {Email}", dto.Email);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequestDto dto)
        {
            var (ok, response) = await _service.RefreshAsync(dto);
            if (!ok || response is null) return Unauthorized();
            return Ok(response);
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var userId))
                {
                    _logger.LogWarning("Logout failed: Invalid user ID claim");
                    return Unauthorized();
                }

                var ok = await _service.LogoutAsync(userId);
                if (!ok)
                {
                    _logger.LogWarning("Logout failed for user ID: {UserId}", userId);
                    return BadRequest("Logout failed");
                }

                _logger.LogInformation("Logout successful for user ID: {UserId}", userId);
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
