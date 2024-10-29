
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

            var likesCount = await _context.Likes.CountAsync(l => l.Id == postId);   //count likes

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

            var AlreadyLiked = await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
            if (AlreadyLiked)
            {
                response.Success = false;
                response.Message = "Already liked";

                return response;
            }

            Like newLike = new Like
            {
                UserId = userId,
                PostId = postId
            };

            _context.Likes.Add(newLike);
            await _context.SaveChangesAsync();

            response.Data = true;

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
