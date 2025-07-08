using Blog.Application.Services;
using Blog.Domain.Exceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public class DeleteBlogPostHandler(IPostRepository repository, ICacheService cache) : IRequestHandler<DeleteBlogPostCommand, bool>
{
    public async Task<bool> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var existingPost = await repository.GetByIdAsync(request.Id);

        if (existingPost is null || existingPost.IsDeleted)
            return false;

        existingPost.IsDeleted = true;
        
        // caching
        var cacheKey = $"post:{request.Id}";
        await cache.RemoveAsync(cacheKey);

        await repository.UpdateAsync(existingPost);

        return true;
    }
}
