using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public record PatchCommentCommand : IRequest<CommentDto>
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
}
