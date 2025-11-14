using DTOs;

namespace Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetAsync(int userId, int courseId);
    Task<IEnumerable<Enrollment>> GetByUserAsync(int userId);
    Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId);
    Task<(IEnumerable<Enrollment> Items, int Total)> GetPagedAsync(int page, int pageSize);

    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task RemoveAsync(Enrollment enrollment);

}
