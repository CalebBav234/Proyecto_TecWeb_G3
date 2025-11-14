using DTOs;
using DTOs.Dtos;
using Repositories;
using AutoMapper;

namespace Services;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo;
    private readonly IMapper _mapper;

    public EnrollmentService(IEnrollmentRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Enrollment?> GetAsync(int userId, int courseId)
    {
        return await _repo.GetAsync(userId, courseId);
    }

    public async Task<IEnumerable<Enrollment>> GetByUserAsync(int userId)
    {
        return await _repo.GetByUserAsync(userId);
    }

    public async Task<IEnumerable<Enrollment>> GetByCourseAsync(int courseId)
    {
        return await _repo.GetByCourseAsync(courseId);
    }

    public async Task<(IEnumerable<Enrollment> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        return await _repo.GetPagedAsync(page, pageSize);
    }

    public async Task<IEnumerable<Enrollment>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Enrollment> CreateEnrollment(CreateEnrollmentDto dto)
    {
        var enrollment = new Enrollment
        {
            UserId = dto.UserId,
            CourseId = dto.CourseId
        };
        await _repo.AddAsync(enrollment);
        return enrollment;
    }

    public async Task<Enrollment> UpdateEnrollment(UpdateEnrollmentDto dto, int userId, int courseId, int currentUserId)
    {
        Enrollment? enrollment = await GetAsync(userId, courseId);
        if (enrollment == null) throw new Exception("Enrollment doesn't exist.");

        if (enrollment.UserId != currentUserId) throw new UnauthorizedAccessException("You are not authorized to update this enrollment.");

        if (dto.Progress.HasValue) enrollment.Progress = dto.Progress.Value;

        await _repo.UpdateAsync(enrollment);
        return enrollment;
    }

    public async Task DeleteEnrollment(int userId, int courseId, int currentUserId)
    {
        Enrollment? enrollment = await GetAsync(userId, courseId);
        if (enrollment == null) return;

        if (enrollment.UserId != currentUserId) throw new UnauthorizedAccessException("You are not authorized to delete this enrollment.");

        await _repo.RemoveAsync(enrollment);
    }
}
