using System.ComponentModel.DataAnnotations;

namespace DTOs.Dtos;
public class ProfileDto
{
    public Guid Id { get; set; }

    [Required]
    public string FullName { get; set; } = null!;

    public string? Bio { get; set; }

    [Url]
    public string? AvatarUrl { get; set; }
}
