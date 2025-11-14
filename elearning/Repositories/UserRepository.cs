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
    }
    public async Task<User?> GetByEmailAsync(string email)
    {
         return await _db.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
    public async Task<User?> GetByIdAsync(int id)
    {
        return await _db.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _db.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
            .ToListAsync();
    }
    public Task Update(User user)
    {
        _db.Users.Update(user);
        return Task.CompletedTask;
    }

    public Task Remove(User user)
    {
        _db.Users.Remove(user);
        return Task.CompletedTask;
    }
    public async Task SaveChangesAsync()
    {
        await _db.SaveChangesAsync();
    }
    
}