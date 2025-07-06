using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public record CreateCommentCommand : IRequest<CommentDto>
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public int PostId { get; set; }
}
