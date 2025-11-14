namespace DTOs;

public class Profile
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }

    public User User { get; set; } = null!;
}
