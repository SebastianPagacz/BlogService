using AutoMapper;
using Blog.Application.Services;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Models;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Queries.CommentQueries;

public class GetCommentByIdHandler(ICommentRepository repository, IMapper mapper, ICacheService cache) : IRequestHandler<GetCommentByIdQuery, CommentDto>
{
    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        //caching 
        var cacheKey = $"comment:{request.Id}";
        var cachedComment = await cache.GetAsync<CommentEntity>(cacheKey);
        if (cachedComment != null && !cachedComment.IsDeleted)
        {
            return mapper.Map<CommentDto>(cachedComment);
        }

        var existingComment = await repository.GetByIdAsync(request.Id);

        if (existingComment is null || existingComment.IsDeleted)
            throw new ItemNotFoundException("Comment was not found");

        await cache.SetAsync(cacheKey, existingComment, TimeSpan.FromMinutes(3));

        return mapper.Map<CommentDto>(existingComment);
    }
}
