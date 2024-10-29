
namespace Grams.Server.Services.LikeService;

public class LikeService : ILikeService
{
    private readonly DataContext _context;

    public LikeService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<int>> GetLikesCount(int postId)
    {
        var response = new ServiceResponse<int>();

        try
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                response.Success = false;
                response.Message = "Not Found";

                return response;
            }

            var likesCount = await _context.Likes.CountAsync(l => l.PostId == postId);   //count likes

            response.Data = likesCount;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<bool>> IsLiked(int userId, int postId)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                response.Success = false;
                response.Message = "Not Found";

                return response;
            }

            response.Data = await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<bool>> LikePost(int userId, int postId)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                response.Success = false;
                response.Message = "Not Found";

                return response;
            }

            var Liked = await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
            if (Liked)
            {
                response.Success = false;
                response.Message = "Post Already liked";

                return response;
            }

            Like newLike = new Like
            {
                UserId = userId,
                PostId = postId
            };

            _context.Likes.Add(newLike);

            post.Likes += 1;

            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Post Liked";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
    public async Task<ServiceResponse<bool>> DislikePost(int userId, int postId)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == postId);
            if (post == null)
            {
                response.Success = false;
                response.Message = "Not Found";

                return response;
            }

            var Liked = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (Liked == null)
            {
                response.Success = false;
                response.Message = "Post not liked";

                return response;
            }

            _context.Likes.Remove(Liked);

            post.Likes -= 1;

            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Post unliked";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

}
