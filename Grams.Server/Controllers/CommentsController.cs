﻿using Microsoft.AspNetCore.Mvc;

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

}