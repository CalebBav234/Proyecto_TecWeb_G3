using Data;
using DTOs;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly  AppDbContext _db;
    public CourseRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(Course course)
    {
        await _db.Courses.AddAsync(course);
        await _db.SaveChangesAsync();
    }
    public async Task<(IEnumerable<Course> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var total = await _db.Courses.CountAsync();
        var items = await _db.Courses
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .OrderByDescending(c => c.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);

    }
    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _db.Courses
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
                .ThenInclude(e => e.User)
            .FirstOrDefaultAsync(c => c.Id == id);
        
    }
    public async Task RemoveAsync(Course course)
    {
        _db.Courses.Remove(course);
        await _db.SaveChangesAsync();
    }
    public async Task UpdateAsync(Course course)
    {
        _db.Courses.Update(course);
        await _db.SaveChangesAsync();
    }
    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _db.Courses
            .AsNoTracking()
            .Include(c => c.Teacher)
            .Include(c => c.Lessons)
            .Include(c => c.Enrollments)
                .ThenInclude(e => e.User)
            .ToListAsync();
    }

}