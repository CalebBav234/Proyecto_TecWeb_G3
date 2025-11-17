using System.ComponentModel.DataAnnotations;

public class CreateLessonDto
{
    [Required]
    public string Title { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    [Required]
    public Guid CourseId { get; set; }
}
