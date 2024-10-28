namespace Grams.Server.DTOs;

public class PostCreate
{
    [Required]
    public IFormFile File { get; set; }
    public string? Caption { get; set; }
}
