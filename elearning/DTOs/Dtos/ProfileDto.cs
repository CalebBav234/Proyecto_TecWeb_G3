using System.ComponentModel.DataAnnotations;

public class ProfileDto
{
    public int Id { get; set; }

    [Required]
    public string FullName { get; set; } = null!;

    public string? Bio { get; set; }

    [Url]
    public string? AvatarUrl { get; set; }
}