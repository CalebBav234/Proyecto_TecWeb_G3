using System.ComponentModel.DataAnnotations;

public class CreateCourseDto
{
    [Required]
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    [Required]
    public int TeacherId { get; set; }
}