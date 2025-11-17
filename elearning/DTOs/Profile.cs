namespace DTOs;

public class Profile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string? Bio { get; set; }
    public string? AvatarUrl { get; set; }

    public User User { get; set; } = null!;
}
