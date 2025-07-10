using AutoMapper;
using Blog.Application.Services;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public class CreateCommentHandler(ICommentRepository commentRepository, IPostRepository postRepository, IMapper mapper, ICacheService cache) : IRequestHandler<CreateCommentCommand, CommentDto>
{
    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var exisitngPost = await postRepository.GetByIdAsync(request.PostId);

        if (exisitngPost is null || exisitngPost.IsDeleted)
            throw new ItemNotFoundException("Coresponding post does not exists");

        var comment = new CommentEntity
        {
            Title = request.Title,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            PostId = request.PostId,
        };

        await commentRepository.AddAsync(comment);

        // caching : does not work idk why
        //var exisitngComment = await commentRepository.GetByIdAsync(comment.Id);
        //var cacheKey = $"comment:{exisitngComment.Id}";
        //await cache.SetAsync(cacheKey, exisitngComment, TimeSpan.FromMinutes(3));

        return mapper.Map<CommentDto>(comment);
    }
}
