using Blog.Application.Commands.ProductCommands;
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
    public async Task<IActionResult> AddAsync(CreateBlogPostDto request)
    {
        var result = await mediator.Send(new CreateBlogPostCommand { Titile = request.Title, Content = request.Content });

        return StatusCode(200, result);
    }

    //[HttpGet]
    //public async Task<IActionResult> GetAll()
    //{
    //    return StatusCode(200);
    //}
}
