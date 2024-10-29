using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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

    [HttpPost("like-post/{id:int}"), Authorize]
    public async Task<ActionResult> LikePost(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _likeService.LikePost(userId, id);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPost("dislike-post/{id:int}"), Authorize]
    public async Task<ActionResult> DislikePost(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _likeService.DislikePost(userId, id);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }
}
