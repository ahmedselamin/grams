﻿
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

    public Task<ServiceResponse<bool>> IsLiked(int userId, int postId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<bool>> LikePost(int userId, int postId)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<bool>> DislikePost(int userId, int postId)
    {
        throw new NotImplementedException();
    }

}
