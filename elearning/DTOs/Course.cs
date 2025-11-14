namespace DTOs;

public class Course
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int TeacherId { get; set; }  
    public User Teacher { get; set; } = null!;

    public ICollection<Lesson>? Lessons { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; } 
}
