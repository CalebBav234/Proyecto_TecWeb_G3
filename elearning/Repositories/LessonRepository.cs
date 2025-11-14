using DTOs;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class LessonRepository : ILessonRepository
{
    private readonly AppDbContext _db;
    public LessonRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(Lesson lesson)
    {
        await _db.Lessons.AddAsync(lesson);
        await _db.SaveChangesAsync();
    }
    public async Task<Lesson?> GetByIdAsync(int id)
    {
        return await _db.Lessons

            .Include(l => l.Course)
            .ThenInclude(c => c.Teacher)
            .FirstOrDefaultAsync(l => l.Id == id);
    }
    public async Task<IEnumerable<Lesson>> GetByCourseAsync(int courseId)
    {
        return await _db.Lessons
            .Where(l => l.CourseId == courseId)
            .OrderBy(l => l.CreatedAt)
            .ToListAsync();
    }
    public async Task RemoveAsync(Lesson lesson)
    {
        _db.Lessons.Remove(lesson);
        await _db.SaveChangesAsync();
        
    }
    public async Task UpdateAsync(Lesson lesson)
    {
        _db.Lessons.Update(lesson);
        await _db.SaveChangesAsync();
    }
    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _db.Lessons
            .AsNoTracking()
            .Include(l => l.Course)
                .ThenInclude(c => c.Teacher)
            .OrderBy(l => l.CreatedAt)
            .ToListAsync();
    }


}
    
