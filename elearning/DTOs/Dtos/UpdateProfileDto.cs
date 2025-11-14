using System.ComponentModel.DataAnnotations;

public class UpdateProfileDto
{
    public string? FullName { get; set; }

    public string? Bio { get; set; }

    [Url]
    public string? AvatarUrl { get; set; }
}