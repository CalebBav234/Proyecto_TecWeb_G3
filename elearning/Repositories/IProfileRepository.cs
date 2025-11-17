using DTOs;

namespace Repositories;

public interface IProfileRepository
{
    Task<Profile?> GetByIdAsync(Guid id);
    Task<Profile?> GetByUserIdAsync(Guid userId);
    Task<(IEnumerable<Profile> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<Profile>> GetAllAsync();
    Task AddAsync(Profile profile);
    Task UpdateAsync(Profile profile);
    Task RemoveAsync(Profile profile);
}
