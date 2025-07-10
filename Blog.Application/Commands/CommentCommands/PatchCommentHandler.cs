using AutoMapper;
using Blog.Application.Services;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public class PatchCommentHandler(ICommentRepository repository, IMapper mapper, ICacheService cache) : IRequestHandler<PatchCommentCommand, CommentDto>
{
    public async Task<CommentDto> Handle(PatchCommentCommand request, CancellationToken cancellationToken)
    {
        var exisitngComment = await repository.GetByIdAsync(request.Id);

        if (exisitngComment is null || exisitngComment.IsDeleted)
            throw new ItemNotFoundException("Comment was not found");

        if(!String.IsNullOrEmpty(request.Title))
            exisitngComment.Title = request.Title;

        if (!String.IsNullOrEmpty(request.Content))
            exisitngComment.Content = request.Content;

        exisitngComment.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(exisitngComment);
        // caching
        var cacheKey = $"comment:{exisitngComment.Id}";
        await cache.SetAsync(cacheKey, exisitngComment, TimeSpan.FromMinutes(3));

        return mapper.Map<CommentDto>(exisitngComment);
    }
}
