using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions.BlogPostExceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Queries.BlogPostQueries;

public class GetAllBlogPostsHandler(IPostRepository repository, IMapper mapper) : IRequestHandler<GetAllBlogPostsQuery, List<BlogPostDto>>
{
    public async Task<List<BlogPostDto>> Handle(GetAllBlogPostsQuery request, CancellationToken cancellationToken)
    {
        var posts = await repository.GetAllAsync();
        var availablePosts = posts.Where(p => !p.IsDeleted);

        if (!availablePosts.Any())
            throw new ItemNotFoundException("Content not found");

        return mapper.Map<List<BlogPostDto>>(availablePosts);
    }
}
