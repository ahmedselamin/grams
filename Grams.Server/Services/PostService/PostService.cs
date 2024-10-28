﻿namespace Grams.Server.Services.PostService;

public class PostService : IPostService
{
    private readonly DataContext _context;

    public PostService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<List<Post>>> GetPosts()
    {
        var response = new ServiceResponse<List<Post>>();

        try
        {
            var posts = await _context.Posts
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            response.Data = posts;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }
    public async Task<ServiceResponse<Post>> GetPost(int PostId)
    {
        var response = new ServiceResponse<Post>();

        try
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == PostId);
            if (post == null)
            {
                response.Success = false;
                response.Message = "Not found";
                return response;
            }

            response.Data = post;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
    }
    public Task<ServiceResponse<Post>> AddPost(int userId, Post post)
    {
        throw new NotImplementedException();
    }
    public Task<ServiceResponse<Post>> UpdatePost(int userId, int postId, Post updatedPost)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<bool>> DeletePost(int userId, int postId)
    {
        throw new NotImplementedException();
    }

}
