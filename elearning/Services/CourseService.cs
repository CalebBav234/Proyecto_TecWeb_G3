using DTOs;
using DTOs.Dtos;
using Repositories;
using AutoMapper;

namespace Services;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;
    private readonly IMapper _mapper;

    public CourseService(ICourseRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<(IEnumerable<Course> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        return await _repo.GetPagedAsync(page, pageSize);
    }

    public async Task<IEnumerable<Course>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Course> CreateCourse(CreateCourseDto dto)
    {
        var course = new Course
        {
            Title = dto.Title,
            Description = dto.Description,
            TeacherId = dto.TeacherId
        };
        await _repo.AddAsync(course);
        return course;
    }

    public async Task<Course> UpdateCourse(UpdateCourseDto dto, int id, int userId)
    {
        Course? course = await GetByIdAsync(id);
        if (course == null) throw new Exception("Course doesn't exist.");

        if (course.TeacherId != userId) throw new UnauthorizedAccessException("You are not authorized to update this course.");

        if (dto.Title != null) course.Title = dto.Title;
        if (dto.Description != null) course.Description = dto.Description;
        if (dto.TeacherId.HasValue) course.TeacherId = dto.TeacherId.Value;

        await _repo.UpdateAsync(course);
        return course;
    }

    public async Task DeleteCourse(int id, int userId)
    {
        Course? course = await GetByIdAsync(id);
        if (course == null) return;

        if (course.TeacherId != userId) throw new UnauthorizedAccessException("You are not authorized to delete this course.");

        await _repo.RemoveAsync(course);
    }
}
