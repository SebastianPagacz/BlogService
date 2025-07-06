using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Queries.CommentQueries;

public record GetCommentByIdQuery : IRequest<CommentDto>
{
    public int Id { get; set; }
}
