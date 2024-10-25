
namespace Grams.Server.Services.ImageService;

public class ImageService : IImageService
{
    private readonly DataContext _context;
    private readonly IWebHostEnvironment _environment;

    public ImageService(DataContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<ServiceResponse<Image>> UploadImage(int userId, IFormFile file)
    {
        var response = new ServiceResponse<Image>();

        try
        {
            // Check for a valid file
            if (file == null || file.Length == 0)
            {
                response.Success = false;
                response.Message = "No file uploaded or file is empty.";
                return response;
            }

            // Ensure the "uploads" folder exists
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

            // Create Image record
            var image = new Image
            {
                FileName = uniqueFileName,
                FilePath = filePath,
                ContentType = file.ContentType,
                CreatedAt = DateTime.Now,
                UserId = userId
            };

            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            response.Data = image;
            response.Message = "Image uploaded";

            return response;

        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }

    public async Task<ServiceResponse<List<Image>>> GetImages()
    {
        var response = new ServiceResponse<List<Image>>();

        try
        {
            var images = await _context.Images
                .OrderByDescending(i => i.CreatedAt)
                .ToListAsync();

            response.Data = images;
            return response;
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;

            return response;
        }

    }

    public async Task<ServiceResponse<bool>> DeleteImage(int userId, int imageId)
    {
        var response = new ServiceResponse<bool>();

        try
        {
            var image = await _context.Images.FirstOrDefaultAsync(i => i.Id == imageId && i.UserId == userId);
            if (image == null)
            {
                response.Success = false;
                response.Message = "Not found";

                return response;
            }

            if (!string.IsNullOrEmpty(image.FilePath) && File.Exists(image.FilePath))  //delete file
            {
                File.Delete(image.FilePath);
            }

            _context.Images.Remove(image);
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
}
