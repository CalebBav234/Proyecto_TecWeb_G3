using System.ComponentModel.DataAnnotations;

public class UpdateEnrollmentDto
{
    [Range(0, 100)]
    public float? Progress { get; set; }
}