using DTOs;

namespace Repositories;

public interface IEnrollmentRepository
{
    Task<Enrollment?> GetAsync(Guid userId, Guid courseId);
    Task<IEnumerable<Enrollment>> GetByUserAsync(Guid userId);
    Task<IEnumerable<Enrollment>> GetByCourseAsync(Guid courseId);
    Task<(IEnumerable<Enrollment> Items, int Total)> GetPagedAsync(int page, int pageSize);

    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task AddAsync(Enrollment enrollment);
    Task UpdateAsync(Enrollment enrollment);
    Task RemoveAsync(Enrollment enrollment);

}
