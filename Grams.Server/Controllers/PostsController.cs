using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Grams.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    [HttpGet("fetch")]
    public async Task<ActionResult> FetchPosts()
    {
        var response = await _postService.GetPosts();
        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpGet("fetch/{id:int}")]
    public async Task<ActionResult> FetchPost(int id)
    {
        var response = await _postService.GetPost(id);
        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

    [HttpPost("create"), Authorize]
    public async Task<ActionResult> CreatePost([FromForm] PostCreate request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _postService.AddPost(userId, request.File, request.Caption);

        return response.Success ? Ok(response) : BadRequest(response.Message);
    }

}
