using System.ComponentModel.DataAnnotations;

namespace DTOs.Dtos;
public class UserDto
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    [EmailAddress]
    public string Email { get; set; } = null!;

    public ProfileDto? Profile { get; set; }
}
