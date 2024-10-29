namespace Grams.Server.Services.LikeService;

public interface ILikeService
{
    Task<ServiceResponse<int>> GetLikesCount(int postId);
    Task<ServiceResponse<bool>> LikePost(int userId, int postId);
    Task<ServiceResponse<bool>> DislikePost(int userId, int postId);
    Task<ServiceResponse<bool>> IsLiked(int userId, int postId);
}
