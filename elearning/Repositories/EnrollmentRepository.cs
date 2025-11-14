using DTOs;
using Data;
using Microsoft.EntityFrameworkCore;

namespace  Repositories;

public class EnrollmentRepository : IEnrollmentRepository
{
    private readonly AppDbContext _db;
    public EnrollmentRepository(AppDbContext db)
    {
        _db = db;
    }
    public async Task AddAsync(Enrollment enrollment)
    {
        await _db.Enrollments.AddAsync(enrollment);
        await _db.SaveChangesAsync();
    }
    public async Task<Enrollment?> GetAsync(int userId, int courseId)
    {
        return await _db.Enrollments
            .Include(e => e.User)
            .Include(e => e.Course)
                .ThenInclude(c => c.Teacher)
            .FirstOrDefaultAsync(e => e.UserId == userId && e.CourseId == courseId);
    }
    public async Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId)
    {
        return await _db.Enrollments
            .Where(e => e.CourseId == courseId)
            .Include(e => e.User)
            .ToListAsync();
    }
    public async Task<IEnumerable<Enrollment>> GetByUserAsync(int userId)
    {
        return await _db.Enrollments
            .Where(e => e.UserId == userId)
            .Include(e => e.Course)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Enrollment> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var total = await _db.Enrollments.CountAsync();
        var items = await _db.Enrollments
            .AsNoTracking()
            .Include(e => e.User)
            .Include(e => e.Course)
                .ThenInclude(c => c.Teacher)
            .OrderBy(e => e.EnrolledAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }
    public async Task UpdateAsync(Enrollment enrollment)
    {
        _db.Enrollments.Update(enrollment);
        await _db.SaveChangesAsync();
    }
    public async Task RemoveAsync(Enrollment enrollment)
    {
        _db.Enrollments.Remove(enrollment);
        await _db.SaveChangesAsync();
        
    }
    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _db.Enrollments
            .AsNoTracking()
            .Include(e => e.User)
            .Include(e => e.Course)
                .ThenInclude(c => c.Teacher)
            .ToListAsync();
    }

}

