using System.ComponentModel.DataAnnotations;

public class LessonDto
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = null!;

    public string Content { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int CourseId { get; set; }
}