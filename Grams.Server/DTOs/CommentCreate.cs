namespace Grams.Server.DTOs;

public class CommentCreate
{
    public int PostId { get; set; }
    public string Content { get; set; } = string.Empty;

}
