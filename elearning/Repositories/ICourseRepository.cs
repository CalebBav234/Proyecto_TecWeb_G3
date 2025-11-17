using DTOs;

namespace Repositories;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(Guid id);
    Task<(IEnumerable<Course> Items, int Total)> GetPagedAsync(int page, int pageSize);

    Task<IEnumerable<Course>> GetAllAsync();
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task RemoveAsync(Course course);
}
