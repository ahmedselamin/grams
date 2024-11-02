
namespace Grams.Server.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public byte[] PasswordHash { get; set; } = new byte[32];
    public byte[] PasswordSalt { get; set; } = new byte[32];
    public List<Post> Posts { get; set; } = new();
    public List<Like> Likes { get; set; } = new();
    public List<Notification> Notifications { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
}
