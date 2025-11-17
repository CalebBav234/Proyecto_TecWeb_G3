using System.ComponentModel.DataAnnotations;

namespace DTOs.Dtos;
public class CourseDto
{
    public Guid Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid TeacherId { get; set; }

    public UserDto Teacher { get; set; } = null!;

    public List<LessonDto> Lessons { get; set; } = new();

    public List<EnrollmentDto> Enrollments { get; set; } = new();
}
