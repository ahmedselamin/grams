namespace Grams.Server.Models;

public class Image
{
    public int Id { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
