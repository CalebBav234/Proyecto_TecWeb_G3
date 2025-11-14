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
         await _db.Users
            .Include(u => u.Profile)
            .Include(u => u.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(u => u.Email == email);
    }
}