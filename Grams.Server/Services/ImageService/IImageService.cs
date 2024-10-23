namespace Grams.Server.Services.ImageService;

public interface IImageService
{
    Task<ServiceResponse<Image>> UploadImage(IFormFile file);
}
