namespace DTOs;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = null!;
    public byte[] PasswordHash { get; set; } = null!;
    public byte[] PasswordSalt { get; set; } = null!;
    public string Role { get; set; } = "User"; 
    public Profile? Profile { get; set; }        
    public ICollection<Course>? CoursesTaught { get; set; } 
    public ICollection<Enrollment>? Enrollments { get; set; } 
}
