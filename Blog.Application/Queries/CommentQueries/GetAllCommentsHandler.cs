using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Queries.CommentQueries;

public class GetAllCommentsHandler(ICommentRepository commentRepository, IPostRepository postRepository, IMapper mapper) : IRequestHandler<GetAllCommentsQuery, List<CommentDto>>
{
    public async Task<List<CommentDto>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var exisitngPost = await postRepository.GetByIdAsync(request.PostId);
        
        if (exisitngPost is null)
            throw new ItemNotFoundException("Coresponding post does not exists");

        var comments = await commentRepository.GetAllAsync(request.PostId);

        return mapper.Map<List<CommentDto>>(comments);
    }
}
