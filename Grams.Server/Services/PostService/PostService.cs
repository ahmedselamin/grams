namespace Grams.Server.Services.PostService;

public class PostService : IPostService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;

    public PostService(DataContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
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
    public async Task<ServiceResponse<Post>> AddPost(int userId, IFormFile file, string? caption)
    {
        var response = new ServiceResponse<Post>();

        try
        {
            // Validate file type
            if (file == null || (file.ContentType != "image/jpeg" && file.ContentType != "image/png"))
            {
                response.Success = false;
                response.Message = "You gotta select a photo. ";
                return response;
            }

            // Ensure uploads folder exists
            var uploadDir = Path.Combine(_environment.WebRootPath ?? "wwwroot", "uploads");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Define a unique file path
            var uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadDir, uniqueFileName);

            // Save the file to disk
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var post = new Post
            {
                UserId = userId,
                FilePath = filePath,
                FileName = uniqueFileName,
                Caption = caption,
                CreatedAt = DateTime.Now
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            response.Data = post;
            response.Message = "New post added successfully.";

            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
            return response;
        }
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
