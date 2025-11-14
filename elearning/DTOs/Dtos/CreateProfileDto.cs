using System.ComponentModel.DataAnnotations;

public class CreateProfileDto
{
    [Required]
    public string FullName { get; set; } = null!;

    public string? Bio { get; set; }

    [Url]
    public string? AvatarUrl { get; set; }
}