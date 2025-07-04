using Blog.Domain.Models;

namespace Blog.Domain.Repositories.Repos;

public interface IPostRepository
{
    Task<BlogPostEntity> AddAsync(BlogPostEntity post);
    Task<List<BlogPostEntity>> GetAllAsync();
    Task<BlogPostEntity> GetByIdAsync(int id);
    Task UpdateAsync(BlogPostEntity post);
}
