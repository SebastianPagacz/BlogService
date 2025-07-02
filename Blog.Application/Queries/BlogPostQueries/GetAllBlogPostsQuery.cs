using Blog.Domain.Dtos;
using MediatR;

namespace Blog.Application.Queries.BlogPostQueries;

public record GetAllBlogPostsQuery : IRequest<List<BlogPostDto>> { }
