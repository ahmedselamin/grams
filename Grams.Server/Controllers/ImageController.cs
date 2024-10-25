using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Grams.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("upload"), Authorize]
    public async Task<ActionResult> UploadImage([FromForm] IFormFile file)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _imageService.UploadImage(userId, file);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpGet("fetch")]
    public async Task<ActionResult> FetchImages()
    {
        var response = await _imageService.GetImages();

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpGet("fetch/{id:int}")]
    public async Task<ActionResult> FetchImage(int id)
    {
        var response = await _imageService.GetImage(id);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpDelete("delete/{id:int}"), Authorize]
    public async Task<ActionResult> DeleteImage(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _imageService.DeleteImage(userId, id);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }
}
