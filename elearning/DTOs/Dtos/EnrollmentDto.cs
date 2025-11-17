using System.ComponentModel.DataAnnotations;

namespace DTOs.Dtos;
public class EnrollmentDto
{
    public Guid UserId { get; set; }

    public Guid CourseId { get; set; }

    public DateTime EnrolledAt { get; set; }

    [Range(0, 100)]
    public float Progress { get; set; }

    public UserDto User { get; set; } = null!;

    public CourseDto Course { get; set; } = null!;
}
