using Blog.Application.Commands.CommentCommands;
using Blog.Application.Queries.CommentQueries;
using Blog.Domain.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("post/{postId}")]
    public async Task<IActionResult> AddAsync(int postId, CreateCommentDto request)
    {
        var result = await mediator.Send(new CreateCommentCommand 
        { 
            Title = request.Title, 
            Content = request.Content, 
            PostId = postId 
        });

        return StatusCode(200, result);
    }

    [HttpGet("post/{postId}")]
    public async Task<IActionResult> GetAllAsync(int postId)
    {
        var result = await mediator.Send(new GetAllCommentsQuery { PostId = postId });

        return StatusCode(200, result);
    }

    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetById(int commentId)
    {
        var result = await mediator.Send(new GetCommentByIdQuery { Id = commentId });

        return StatusCode(200, result);
    }

    [HttpPatch("{commentId}")]
    public async Task<IActionResult> PatchAsync(int commentId, [FromBody]CreateCommentDto request)
    {
        var result = await mediator.Send(new PatchCommentCommand { Id = commentId, Title = request.Title, Content = request.Content });

        return StatusCode(200, result);
    }

    [HttpDelete("{commentId}")]
    public async Task<IActionResult> DeleteAsync(int commentId)
    {
        var result = await mediator.Send(new DeleteCommentCommand { Id = commentId });
        if (result == true)
            return StatusCode(200, new { message = "Comment deleted" });

        return StatusCode(404, new { message = "Comment was not found" });
    }
}
