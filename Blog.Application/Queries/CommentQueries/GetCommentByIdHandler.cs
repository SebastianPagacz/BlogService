using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Queries.CommentQueries;

public class GetCommentByIdHandler(ICommentRepository repository, IMapper mapper) : IRequestHandler<GetCommentByIdQuery, CommentDto>
{
    public async Task<CommentDto> Handle(GetCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var existingComment = await repository.GetByIdAsync(request.Id);

        if (existingComment is null || existingComment.IsDeleted)
            throw new ItemNotFoundException("Comment was not found");

        return mapper.Map<CommentDto>(existingComment);
    }
}
