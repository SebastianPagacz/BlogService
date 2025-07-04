using Blog.Application.Commands.BlogPostCommands;
using Blog.Application.Commands.ProductCommands;
using Blog.Application.Queries.BlogPostQueries;
using Blog.Domain.Dtos;
using Blog.Domain.Repositories.Repos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateBlogPostDto request)
    {
        var result = await mediator.Send(new CreateBlogPostCommand { Titile = request.Title, Content = request.Content });

        return StatusCode(200, result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await mediator.Send(new GetAllBlogPostsQuery());

        return StatusCode(200, result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await mediator.Send(new GetBlogPostByIdQuery { Id = id });

        return StatusCode(200, result);
    }
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchAsync(int id, [FromBody] CreateBlogPostDto request)
    {
        var result = await mediator.Send(new PatchBlogPostCommand { Id = id, Title = request.Title, Content = request.Content });

        return StatusCode(200, result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await mediator.Send(new DeleteBlogPostCommand { Id = id });

        if (result == true)
            return StatusCode(200, new { message = "Succesfully deleted"});

        return StatusCode(404, new { message = "Content was not found" });
    }
}
