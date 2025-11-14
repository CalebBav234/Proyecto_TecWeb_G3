using DTOs;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(User user)
    {
        await _db.Users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _db.Users
            .AsNoTracking()
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByRefreshToken(string refreshToken)
    {
        return await _db.Users
            .AsNoTracking()
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _db.Users
            .AsNoTracking()
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<(IEnumerable<User> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var total = await _db.Users.CountAsync();
        var items = await _db.Users
            .AsNoTracking()
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
            .OrderBy(u => u.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.Users
            .AsNoTracking()
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
                .ThenInclude(e => e.Course)
            .ToListAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _db.Users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task RemoveAsync(User user)
    {
        _db.Users.Remove(user);
        await _db.SaveChangesAsync();
    }
}
