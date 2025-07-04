using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public record PatchBlogPostCommand : IRequest<BlogPostDto>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}
