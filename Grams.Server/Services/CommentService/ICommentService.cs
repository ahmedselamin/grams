﻿namespace Grams.Server.Services.CommentService;

public interface ICommentService
{
    Task<ServiceResponse<List<Comment>>> GetComments(int postId);
    Task<ServiceResponse<int>> GetCommentsCount(int postId);
    Task<ServiceResponse<Comment>> GetComment(int commentId);
    Task<ServiceResponse<Comment>> CreateComment(int userId, int postId, string content);
    Task<ServiceResponse<Comment>> UpdateComment(int userId, int commentId, string updatedContent);
    Task<ServiceResponse<bool>> DeleteComment(int userId, int commentId);
}
