using AutoMapper;
using Blog.Application.Services;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Queries.BlogPostQueries;

public class GetBlogPostByIdHandler(IPostRepository repository, IMapper mapper, ICacheService cache) : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        // caching
        var cacheKey = $"post:{request.Id}";
        var cachedPost = await cache.GetAsync<BlogPostEntity>(cacheKey);
        
        if (cachedPost != null && !cachedPost.IsDeleted) 
        {
            return mapper.Map<BlogPostDto>(cachedPost);
        }
        
        var result = await repository.GetByIdAsync(request.Id);

        if (result is null || result.IsDeleted)
            throw new ItemNotFoundException("Content was not found");
        
        // caching
        await cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5));

        return mapper.Map<BlogPostDto>(result);
    }
}
