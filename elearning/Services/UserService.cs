using DTOs;
using DTOs.Dtos;
using Repositories;
using AutoMapper;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public UserService(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }
    

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _repo.GetByEmailAsync(email);
    }

    public async Task<User?> GetByRefreshToken(string refreshToken)
    {
        return await _repo.GetByRefreshToken(refreshToken);
    }

    public async Task<(IEnumerable<User> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        return await _repo.GetPagedAsync(page, pageSize);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<User> AddAsync(RegisterUserDto dto)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
            Role = "User"
        };
        await _repo.AddAsync(user);
        return user;
    }

    public async Task<User> UpdateAsync(UserDto dto)
    {
        User? user = await GetByIdAsync(dto.Id);
        if (user == null) throw new Exception("User doesnt exist.");
        user.Username = dto.Username;
        user.Email = dto.Email;
        await _repo.UpdateAsync(user);
        return user;
    }

    public async Task RemoveAsync(Guid id)
    {
        User? user = await GetByIdAsync(id);
        if (user == null) return;

        await _repo.RemoveAsync(user);
    }
}
