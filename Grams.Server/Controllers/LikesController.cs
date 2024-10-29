using Microsoft.AspNetCore.Mvc;

namespace Grams.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LikesController : ControllerBase
{
    private readonly ILikeService _likeService;

    public LikesController(ILikeService likeService)
    {
        _likeService = likeService;
    }

    [HttpGet("count/{id:int}")]
    public async Task<ActionResult> GetLikesCount(int id)
    {
        var response = await _likeService.GetLikesCount(id);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }
}
