using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Exceptions.BlogPostExceptions;
using Blog.Domain.Repositories.Repos;
using MediatR;

namespace Blog.Application.Commands.BlogPostCommands;

public class PatchBlogPostHandler(IPostRepository repository, IMapper mapper) : IRequestHandler<PatchBlogPostCommand, BlogPostDto>
{
    public async Task<BlogPostDto> Handle(PatchBlogPostCommand request, CancellationToken cancellationToken)
    {
        var existingPost = await repository.GetByIdAsync(request.Id);

        if (existingPost is null)
            throw new ItemNotFoundException("Content was not found");

        if(!String.IsNullOrEmpty(request.Title))
        {
            existingPost.Title = request.Title;
        }

        if (!String.IsNullOrEmpty(request.Content))
        {
            existingPost.Content = request.Content;
        }

        existingPost.UpdatedAt = DateTime.UtcNow;

        await repository.UpdateAsync(existingPost);
        return mapper.Map<BlogPostDto>(existingPost);
    }
}
