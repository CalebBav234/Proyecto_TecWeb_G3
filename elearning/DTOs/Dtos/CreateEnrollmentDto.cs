using System.ComponentModel.DataAnnotations;

public class CreateEnrollmentDto
{
    [Required]
    public Guid UserId { get; set; }

    [Required]
    public Guid CourseId { get; set; }
}
