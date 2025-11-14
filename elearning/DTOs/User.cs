namespace DTOs;

public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public string Role { get; set; } = "User";
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiresAt { get; set; }
    public DateTime? RefreshTokenRevokedAt { get; set; }
    public string? CurrentJwtId { get; set; }
    public Profile? Profile { get; set; }
    public ICollection<Course> CoursesTaught { get; set; } = new List<Course>();
    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
