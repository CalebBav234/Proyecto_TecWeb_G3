using System.ComponentModel.DataAnnotations;

public class CreateEnrollmentDto
{
    [Required]
    public int UserId { get; set; }

    [Required]
    public int CourseId { get; set; }
}