
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
            if (file == null)
            {
                response.Success = false;
                response.Message = "Cannot upload null";
                return response;
            }

            // Validate file 
            var validImageTypes = new List<string> { "image/jpeg", "image/png" };
            if (!validImageTypes.Contains(file.ContentType))
            {
                response.Success = false;
                response.Message = "Invalid file type.";
                return response;
            }

            // Ensure uploads folder exists
            var uploads = Path.Combine(_environment.WebRootPath, "Uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            // Generate unique file name
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploads, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }


            var image = new Image
            {
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


    public Task<ServiceResponse<Image>> GetImages(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResponse<bool>> DeleteImage(int imageId)
    {
        throw new NotImplementedException();
    }
}
