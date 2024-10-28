using Microsoft.AspNetCore.Mvc;

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

    [HttpGet]
    public async Task<ActionResult> FetchPosts()
    {
        var response = await _postService.GetPosts();
        return response.Success ? Ok(response) : BadRequest(response.Message);
    }
}
