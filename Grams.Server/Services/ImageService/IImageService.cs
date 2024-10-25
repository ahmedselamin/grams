namespace Grams.Server.Services.ImageService;

public interface IImageService
{
    Task<ServiceResponse<List<Image>>> GetImages(int userId);
    Task<ServiceResponse<Image>> UploadImage(int userId, IFormFile file);
    Task<ServiceResponse<bool>> DeleteImage(int imageId);
}
