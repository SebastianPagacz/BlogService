using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public record DeleteCommentCommand : IRequest<bool>
{
    public int Id { get; set; }
}
