using DTOs.Dtos;

namespace Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterUserDto dto);
    Task<(bool ok, LoginResponseDto? response)> LoginAsync(LoginUserDto dto);
    Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto);
    Task<bool> LogoutAsync(Guid userId);
}
