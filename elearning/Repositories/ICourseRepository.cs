using DTOs;

namespace Repositories;

public interface ICourseRepository
{
    Task<Course?> GetByIdAsync(int id);
    Task<(IEnumerable<Course> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task AddAsync(Course course);
    Task UpdateAsync(Course course);
    Task RemoveAsync(Course course);
}