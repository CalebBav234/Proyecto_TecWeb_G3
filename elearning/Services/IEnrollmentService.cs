using DTOs;
using DTOs.Dtos;

namespace Services;

public interface IEnrollmentService
{
    Task<Enrollment?> GetAsync(Guid userId, Guid courseId);
    Task<IEnumerable<Enrollment>> GetByUserAsync(Guid userId);
    Task<IEnumerable<Enrollment>> GetByCourseAsync(Guid courseId);
    Task<(IEnumerable<Enrollment> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment> CreateEnrollment(CreateEnrollmentDto dto);
    Task<Enrollment> UpdateEnrollment(UpdateEnrollmentDto dto, Guid userId, Guid courseId, Guid currentUserId);
    Task DeleteEnrollment(Guid userId, Guid courseId, Guid currentUserId);
}
