using AutoMapper;
using Blog.Application.Services;
using Blog.Domain.Exceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public class DeleteCommentHandler(ICommentRepository repository, ICacheService cache) : IRequestHandler<DeleteCommentCommand, bool>
{
    public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var exisitingComment = await repository.GetByIdAsync(request.Id);

        if (exisitingComment is null || exisitingComment.IsDeleted)
            throw new ItemNotFoundException("Comment was not found");

        exisitingComment.IsDeleted = true;
        await repository.UpdateAsync(exisitingComment);

        // caching
        var cachceKey = $"comment:{request.Id}";
        await cache.RemoveAsync(cachceKey);

        return true;
    }
}
