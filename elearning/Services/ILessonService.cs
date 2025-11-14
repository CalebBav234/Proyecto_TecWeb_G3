using DTOs;
using DTOs.Dtos;

namespace Services;

public interface ILessonService
{
    Task<Lesson?> GetByIdAsync(int id);
    Task<IEnumerable<Lesson>> GetByCourseAsync(int courseId);
    Task<IEnumerable<Lesson>> GetAllAsync();
    Task<Lesson> CreateLesson(CreateLessonDto dto);
    Task<Lesson> UpdateLesson(UpdateLessonDto dto, int id, int userId);
    Task DeleteLesson(int id, int userId);
}
