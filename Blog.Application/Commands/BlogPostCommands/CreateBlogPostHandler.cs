using AutoMapper;
using Blog.Application.Commands.ProductCommands;
using Blog.Domain.Dtos;
using Blog.Domain.Models;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public class CreateBlogPostHandler(IPostRepository repository, IMapper mapper) : IRequestHandler<CreateBlogPostCommand, BlogPostDto>
{
    public async Task<BlogPostDto> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
    {
        var newPost = new BlogPostEntity
        {
            Title = request.Titile,
            Content = request.Content,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false,
        };

        await repository.AddAsync(newPost);

        var result = mapper.Map<BlogPostDto>(newPost);

        return result;
    }
}
