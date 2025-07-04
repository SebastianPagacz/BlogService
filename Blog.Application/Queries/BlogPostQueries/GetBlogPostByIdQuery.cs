using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Queries.BlogPostQueries;

public record GetBlogPostByIdQuery : IRequest<BlogPostDto>
{
    public int Id { get; set; }
}
