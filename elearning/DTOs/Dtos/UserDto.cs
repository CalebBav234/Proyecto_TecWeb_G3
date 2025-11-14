using System.ComponentModel.DataAnnotations;
 

public class UserDto
{
    public int Id { get; set; }

    [EmailAddress]
    public string Email { get; set; } = null!;

    public string Role { get; set; } = "User";

    public ProfileDto? Profile { get; set; }
}