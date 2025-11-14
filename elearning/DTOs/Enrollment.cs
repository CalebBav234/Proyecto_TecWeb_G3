namespace DTOs;

public class Enrollment
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public float Progress { get; set; } = 0f;

    public User User { get; set; } = null!;
    public Course Course { get; set; } = null!;
}
