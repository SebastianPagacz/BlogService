using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.CommentCommands;

public class CreateCommentHandler(ICommentRepository commentRepository, IPostRepository postRepository, IMapper mapper) : IRequestHandler<CreateCommentCommand, CommentDto>
{
    public async Task<CommentDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var exisitngPost = await postRepository.GetByIdAsync(request.PostId);

        if (exisitngPost is null)
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

        return mapper.Map<CommentDto>(comment);
    }
}
