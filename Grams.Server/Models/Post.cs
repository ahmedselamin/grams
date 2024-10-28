namespace Grams.Server.Models;

public class Post
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ImageId { get; set; }
    public string? Caption { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
