using DTOs;
using Data;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly AppDbContext _db;

    public ProfileRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Profile profile)
    {
        await _db.Profiles.AddAsync(profile);
        await _db.SaveChangesAsync();
    }

    public async Task<Profile?> GetByIdAsync(int id)
    {
        return await _db.Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Profile?> GetByUserIdAsync(Guid userId)
    {
        return await _db.Profiles
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task<(IEnumerable<Profile> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var total = await _db.Profiles.CountAsync();
        var items = await _db.Profiles
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return (items, total);
    }

    public async Task UpdateAsync(Profile profile)
    {
        _db.Profiles.Update(profile);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<Profile>> GetAllAsync()
    {
        return await _db.Profiles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task RemoveAsync(Profile profile)
    {
        _db.Profiles.Remove(profile);
        await _db.SaveChangesAsync();
    }
}
