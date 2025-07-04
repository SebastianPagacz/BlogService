using Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Repositories.Repos;

public class PostRepository(DataContext context) : IPostRepository
{
    public async Task<BlogPostEntity> AddAsync(BlogPostEntity post)
    {
        await context.BlogPosts.AddAsync(post);
        await context.SaveChangesAsync();
        return post;
    }

    public async Task<List<BlogPostEntity>> GetAllAsync()
    {
        return await context.BlogPosts.ToListAsync();
    }

    public async Task<BlogPostEntity> GetByIdAsync(int id)
    {
        return await context.BlogPosts.FirstOrDefaultAsync(bp => bp.Id == id);
    }

    public async Task UpdateAsync(BlogPostEntity post)
    {
        await context.SaveChangesAsync();
    }
    public async Task<BlogPostEntity> GetByTitleAsync(string title)
    {
        return await context.BlogPosts.FirstOrDefaultAsync(bp => bp.Title == title);
    }
}
