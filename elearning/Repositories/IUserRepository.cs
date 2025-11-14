using DTOs;

namespace Repositories;

public interface IUserRepository
{
    Task<User?> GetByIdAsync(Guid id);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByRefreshToken(string refreshToken);
    Task<(IEnumerable<User> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    Task RemoveAsync(User user);
}
