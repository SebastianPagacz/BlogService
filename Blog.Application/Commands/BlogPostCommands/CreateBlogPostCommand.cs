using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Commands.ProductCommands;

public record CreateBlogPostCommand : IRequest<BlogPostDto>
{
    public string Titile { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
