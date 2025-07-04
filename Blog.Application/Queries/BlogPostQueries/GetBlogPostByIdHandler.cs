using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions.BlogPostExceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Queries.BlogPostQueries;

public class GetBlogPostByIdHandler(IPostRepository repository, IMapper mapper) : IRequestHandler<GetBlogPostByIdQuery, BlogPostDto>
{
    public async Task<BlogPostDto> Handle(GetBlogPostByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await repository.GetByIdAsync(request.Id);

        if (result is null || result.IsDeleted)
            throw new ItemNotFoundException("Content was not found");

        var resultDto = mapper.Map<BlogPostDto>(result);
        return resultDto;
    }
}
