using DTOs;

namespace Repositories;

public interface ILessonRepository
{
    Task<Lesson?> GetByIdAsync(Guid id);
    Task<IEnumerable<Lesson>> GetByCourseAsync(Guid courseId);
    Task<(IEnumerable<Lesson> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task AddAsync(Lesson lesson);
    Task UpdateAsync(Lesson lesson);
    Task RemoveAsync(Lesson lesson);

    Task <IEnumerable<Lesson>> GetAllAsync();
}
