
namespace Grams.Server.Services.LikeService;

public class LikeService : ILikeService
{
    private readonly DataContext _context;
    private readonly INotificationService _notificationService;

    public LikeService(DataContext context, INotificationService notificationService)
    {
        _context = context;
        _notificationService = notificationService;
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

            var liked = await _context.Likes.AnyAsync(l => l.PostId == postId && l.UserId == userId);
            if (liked)
            {
                response.Success = false;
                response.Message = "Post Already liked";
                return response;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            var newLike = new Like
            {
                UserId = userId,
                PostId = postId
            };

            await _context.Likes.AddAsync(newLike);

            var message = $"{user.Username} Liked your photo.";
            await _notificationService.SendNotification(post.UserId, message);

            post.Likes++;

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

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
                return response;
            }

            var liked = await _context.Likes.FirstOrDefaultAsync(l => l.PostId == postId && l.UserId == userId);
            if (liked == null)
            {
                response.Success = false;
                response.Message = "Post not liked";
                return response;
            }


            var message = $"{user.Username} disliked your photo.";
            await _notificationService.SendNotification(post.UserId, message);

            _context.Likes.Remove(liked);

            if (post.Likes > 0)
            {
                post.Likes--;
            }

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
