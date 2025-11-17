using DTOs;
using DTOs.Dtos;

namespace Services;

public interface IProfileService
{
    Task<ProfileDto?> GetByIdAsync(Guid id);
    Task<ProfileDto?> GetByUserIdAsync(Guid userId);
    Task<(IEnumerable<ProfileDto> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<ProfileDto>> GetAllAsync();
    Task<ProfileDto> CreateAsync(CreateProfileDto dto, Guid userId);
    Task<ProfileDto> UpdateAsync(Guid id, UpdateProfileDto dto, Guid userId);
    Task DeleteAsync(Guid id, Guid userId);
}
