namespace Grams.Server.Services.CommentService;

public class CommentService : ICommentService
{
    private readonly DataContext _context;

    public CommentService(DataContext context)
    {
        _context = context;
    }
    public async Task<ServiceResponse<List<Comment>>> GetComments(int postId)
    {
        var response = new ServiceResponse<List<Comment>>();

        try
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            response.Data = comments;

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<Comment>> GetComment(int commentId)
    {
        var response = new ServiceResponse<Comment>();

        try
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentId);
            if (comment == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            response.Data = comment;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<Comment>> CreateComment(int userId, int postId, string content)
    {
        var response = new ServiceResponse<Comment>();

        try
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                response.Success = false;
                response.Message = "Comment content cannot be empty.";
                return response;
            }

            var comment = new Comment
            {
                UserId = userId,
                PostId = postId,
                Content = content,
                CreatedAt = DateTime.Now
            };

            await _context.Comments.AddAsync(comment);
            await _context.SaveChangesAsync();

            response.Data = comment;

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<Comment>> UpdateComment(int userId, int commentId, string updatedContent)
    {
        var response = new ServiceResponse<Comment>();

        try
        {
            if (string.IsNullOrWhiteSpace(updatedContent))
            {
                response.Success = false;
                response.Message = "Content cannot be empty.";
                return response;
            }

            var comment = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentId && p.UserId == userId);
            if (comment == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            comment.Content = updatedContent;

            await _context.SaveChangesAsync();

            response.Data = comment;
            response.Message = "Comment updated";

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }

    public async Task<ServiceResponse<bool>> DeleteComment(int userId, int commentId)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(p => p.Id == commentId && p.UserId == userId);
            if (comment == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            response.Data = true;
            response.Message = "Comment deleted";

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
