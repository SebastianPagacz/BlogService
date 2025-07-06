using Blog.Domain.Exceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public class DeleteBlogPostHandler(IPostRepository repository) : IRequestHandler<DeleteBlogPostCommand, bool>
{
    public async Task<bool> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
    {
        var existingPost = await repository.GetByIdAsync(request.Id);

        if (existingPost is null || existingPost.IsDeleted)
            return false;

        existingPost.IsDeleted = true;
        await repository.UpdateAsync(existingPost);

        return true;
    }
}
