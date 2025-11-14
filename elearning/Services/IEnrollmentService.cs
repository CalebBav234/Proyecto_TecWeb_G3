using DTOs;
using DTOs.Dtos;

namespace Services;

public interface IEnrollmentService
{
    Task<Enrollment?> GetAsync(int userId, int courseId);
    Task<IEnumerable<Enrollment>> GetByUserAsync(int userId);
    Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId);
    Task<(IEnumerable<Enrollment> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<Enrollment>> GetAllAsync();
    Task<Enrollment> CreateEnrollment(CreateEnrollmentDto dto);
    Task<Enrollment> UpdateEnrollment(UpdateEnrollmentDto dto, int userId, int courseId, int currentUserId);
    Task DeleteEnrollment(int userId, int courseId, int currentUserId);
}
