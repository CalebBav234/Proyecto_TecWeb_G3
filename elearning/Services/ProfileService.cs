using DTOs;
using DTOs.Dtos;
using Repositories;
using AutoMapper;

namespace Services;

public class ProfileService : IProfileService
{
    private readonly IProfileRepository _repo;
    private readonly IMapper _mapper;

    public ProfileService(IProfileRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<ProfileDto?> GetByIdAsync(int id)
    {
        var profile = await _repo.GetByIdAsync(id);
        return profile == null ? null : _mapper.Map<ProfileDto>(profile);
    }

    public async Task<ProfileDto?> GetByUserIdAsync(Guid userId)
    {
        var profile = await _repo.GetByUserIdAsync(userId);
        return profile == null ? null : _mapper.Map<ProfileDto>(profile);
    }

    public async Task<(IEnumerable<ProfileDto> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        var (items, total) = await _repo.GetPagedAsync(page, pageSize);
        var dtos = _mapper.Map<IEnumerable<ProfileDto>>(items);
        return (dtos, total);
    }

    public async Task<ProfileDto> CreateAsync(CreateProfileDto dto, Guid userId)
    {
        var existing = await _repo.GetByUserIdAsync(userId);
        if (existing != null) throw new Exception("Profile already exists for this user.");

        var profile = _mapper.Map<DTOs.Profile>(dto);
        profile.UserId = userId;
        await _repo.AddAsync(profile);
        return _mapper.Map<ProfileDto>(profile);
    }

    public async Task<ProfileDto> UpdateAsync(int id, UpdateProfileDto dto, Guid userId)
    {
        var profile = await _repo.GetByIdAsync(id);
        if (profile == null) throw new Exception("Profile not found.");
        if (profile.UserId != userId) throw new UnauthorizedAccessException("You can only update your own profile.");

        _mapper.Map(dto, profile);
        await _repo.UpdateAsync(profile);
        return _mapper.Map<ProfileDto>(profile);
    }

    public async Task<IEnumerable<ProfileDto>> GetAllAsync()
    {
        var profiles = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<ProfileDto>>(profiles);
    }

    public async Task DeleteAsync(int id, Guid userId)
    {
        var profile = await _repo.GetByIdAsync(id);
        if (profile == null) return;
        if (profile.UserId != userId) throw new UnauthorizedAccessException("You can only delete your own profile.");

        await _repo.RemoveAsync(profile);
    }
}
