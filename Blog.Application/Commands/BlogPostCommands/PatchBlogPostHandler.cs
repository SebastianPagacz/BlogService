using AutoMapper;
using Blog.Application.Services;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public class PatchBlogPostHandler(IPostRepository repository, IMapper mapper, ICacheService cache) : IRequestHandler<PatchBlogPostCommand, BlogPostDto>
{
    public async Task<BlogPostDto> Handle(PatchBlogPostCommand request, CancellationToken cancellationToken)
    {
        var existingPost = await repository.GetByIdAsync(request.Id);

        if (existingPost is null || existingPost.IsDeleted)
            throw new ItemNotFoundException("Content was not found");

        if(!String.IsNullOrEmpty(request.Title))
            existingPost.Title = request.Title;

        if (!String.IsNullOrEmpty(request.Content))
            existingPost.Content = request.Content;

        existingPost.UpdatedAt = DateTime.UtcNow;

        //caching
        var cacheKey = $"post:{existingPost.Id}";
        await cache.SetAsync<BlogPostEntity>(cacheKey, existingPost, TimeSpan.FromMinutes(5));

        await repository.UpdateAsync(existingPost);
        return mapper.Map<BlogPostDto>(existingPost);
    }
}
