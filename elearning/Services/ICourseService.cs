using DTOs;
using DTOs.Dtos;

namespace Services;

public interface ICourseService
{
    Task<Course?> GetByIdAsync(int id);
    Task<(IEnumerable<Course> Items, int Total)> GetPagedAsync(int page, int pageSize);
    Task<IEnumerable<Course>> GetAllAsync();
    Task<Course> CreateCourse(CreateCourseDto dto);
    Task<Course> UpdateCourse(UpdateCourseDto dto, int id, int userId);
    Task DeleteCourse(int id, int userId);
}
