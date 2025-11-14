using DTOs;

namespace Repositories;

public interface ILessonRepository
{
    Task<Lesson?> GetByIdAsync(int id);
    Task<IEnumerable<Lesson>> GetByCourseAsync(int courseId);
    Task<(IEnumerable<Lesson> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task AddAsync(Lesson lesson);
    Task UpdateAsync(Lesson lesson);
    Task RemoveAsync(Lesson lesson);

    Task <IEnumerable<Lesson>> GetAllAsync();
}
