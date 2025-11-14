using DTOs;
namespace Repositories;
public interface IUserRepository
{
    Task<User?> GetByIdAsync(int id);
    Task<User?> GetByEmailAsync(string email);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    Task Update(User user);
    Task Remove(User user);
    Task SaveChangesAsync();
}