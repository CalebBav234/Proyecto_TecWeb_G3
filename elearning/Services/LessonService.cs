using DTOs;
using DTOs.Dtos;
using Repositories;
using AutoMapper;

namespace Services;

public class LessonService : ILessonService
{
    private readonly ILessonRepository _repo;
    private readonly ICourseRepository _courseRepo;
    private readonly IMapper _mapper;

    public LessonService(ILessonRepository repo, ICourseRepository courseRepo, IMapper mapper)
    {
        _repo = repo;
        _courseRepo = courseRepo;
        _mapper = mapper;
    }

    public async Task<Lesson?> GetByIdAsync(int id)
    {
        return await _repo.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Lesson>> GetByCourseAsync(int courseId)
    {
        return await _repo.GetByCourseAsync(courseId);
    }

    public async Task<(IEnumerable<Lesson> Items, int Total)> GetPagedAsync(int page, int pageSize)
    {
        return await _repo.GetPagedAsync(page, pageSize);
    }

    public async Task<IEnumerable<Lesson>> GetAllAsync()
    {
        return await _repo.GetAllAsync();
    }

    public async Task<Lesson> CreateLesson(CreateLessonDto dto)
    {
        var lesson = new Lesson
        {
            Title = dto.Title,
            Content = dto.Content,
            CourseId = dto.CourseId
        };
        await _repo.AddAsync(lesson);
        return lesson;
    }

    public async Task<Lesson> UpdateLesson(UpdateLessonDto dto, int id, int userId)
    {
        Lesson? lesson = await GetByIdAsync(id);
        if (lesson == null) throw new Exception("Lesson doesn't exist.");

        
        Course? course = await _courseRepo.GetByIdAsync(lesson.CourseId);
        if (course == null || course.TeacherId != userId) throw new UnauthorizedAccessException("You are not authorized to update this lesson.");

        if (dto.Title != null) lesson.Title = dto.Title;
        if (dto.Content != null) lesson.Content = dto.Content;
        if (dto.CourseId.HasValue) lesson.CourseId = dto.CourseId.Value;

        await _repo.UpdateAsync(lesson);
        return lesson;
    }

    public async Task DeleteLesson(int id, int userId)
    {
        Lesson? lesson = await GetByIdAsync(id);
        if (lesson == null) return;

        
        Course? course = await _courseRepo.GetByIdAsync(lesson.CourseId);
        if (course == null || course.TeacherId != userId) throw new UnauthorizedAccessException("You are not authorized to delete this lesson.");

        await _repo.RemoveAsync(lesson);
    }
}
