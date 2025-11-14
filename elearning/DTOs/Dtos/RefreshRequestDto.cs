using System.ComponentModel.DataAnnotations;

namespace DTOs.Dtos;
public class RefreshRequestDto
{
    [Required]
    public string RefreshToken { get; set; } = null!;
}
