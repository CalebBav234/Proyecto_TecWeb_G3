using DTOs;
using DTOs.Dtos;

namespace Services;

public interface ILessonService
{
    Task<Lesson?> GetByIdAsync(Guid id);
    Task<IEnumerable<Lesson>> GetByCourseAsync(Guid courseId);
    Task<(IEnumerable<Lesson> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<Lesson> CreateLesson(CreateLessonDto dto);
    Task<Lesson> UpdateLesson(UpdateLessonDto dto, Guid id, Guid userId);
    Task DeleteLesson(Guid id, Guid userId);
}
