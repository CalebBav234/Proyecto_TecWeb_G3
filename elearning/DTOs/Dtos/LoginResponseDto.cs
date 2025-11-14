using System.ComponentModel.DataAnnotations;

namespace DTOs.Dtos;
public class LoginResponseDto
{
    public UserDto User { get; set; } = null!;
    public string Role { get; set; } = null!;
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public string TokenType { get; set; } = "Bearer";
}
