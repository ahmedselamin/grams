namespace Grams.Server.Services.PostService;

public interface IPostService
{
    Task<ServiceResponse<List<Post>>> GetPosts();
    Task<ServiceResponse<Post>> GetPost(int PostId);
    Task<ServiceResponse<Post>> AddPost(int userId, IFormFile file, string? caption);
    Task<ServiceResponse<Post>> UpdatePost(int userId, int postId, Post updatedPost);
    Task<ServiceResponse<bool>> DeletePost(int userId, int postId);
}
