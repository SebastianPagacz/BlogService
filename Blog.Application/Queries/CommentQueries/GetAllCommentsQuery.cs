using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Queries.CommentQueries;

public record GetAllCommentsQuery : IRequest<List<CommentDto>>
{
    public int PostId { get; set; }
}
