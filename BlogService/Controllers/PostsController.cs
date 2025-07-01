using Blog.Domain.Repositories.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PostsController(IPostRepository repo) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await repo.GetAllAsync();
        return StatusCode(200, result);
    }
}
