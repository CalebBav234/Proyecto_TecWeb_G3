using DTOs;
using DTOs.Dtos;

namespace Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshToken(string refreshToken);
    Task<(IEnumerable<User> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<User>> GetAllAsync();

    Task<User> AddAsync(RegisterUserDto dto);
    Task<User> UpdateAsync(UserDto dto);
    Task RemoveAsync(Guid id);
}
