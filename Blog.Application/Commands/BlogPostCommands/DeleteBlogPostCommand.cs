using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public record DeleteBlogPostCommand : IRequest<bool>
{
    public int Id { get; set; }
}
