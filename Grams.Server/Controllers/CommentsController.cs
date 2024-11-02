using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Grams.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly CommentService _commentService;

    public CommentsController(CommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("fetch-comments")]
    public async Task<ActionResult> FetchComments(int postId)
    {
        var response = await _commentService.GetComments(postId);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("fetch-comment/{id:int}")]
    public async Task<ActionResult> FetchComment(int id)
    {
        var response = await _commentService.GetComment(id);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost("add-comment"), Authorize]
    public async Task<ActionResult> CreateComment([FromBody] CommentCreate request)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _commentService.CreateComment(userId, request.PostId, request.Content);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPatch("edit-comment/{id:int}"), Authorize]
    public async Task<ActionResult> EditComment(int id, [FromBody] string updatedContent)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var response = await _commentService.UpdateComment(userId, id, updatedContent);

        return response.Success ? Ok(response) : BadRequest(response);
    }



}
