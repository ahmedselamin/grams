
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

    public async Task<ServiceResponse<Comment>> UpdateComment(int userId, int postId, string updatedContent)
    {
        var response = new ServiceResponse<Comment>();

        try
        {

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

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }
    }
}
