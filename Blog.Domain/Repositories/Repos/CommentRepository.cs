using Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Repositories.Repos;

public class CommentRepository(DataContext context) : ICommentRepository
{
    public async Task<CommentEntity> AddAsync(CommentEntity comment)
    {
        await context.Comments.AddAsync(comment);
        await context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<CommentEntity>> GetAllAsync()
    {
        return await context.Comments.ToListAsync();
    }

    public async Task<CommentEntity> GetByIdAsync(int id)
    {
        return await context.Comments.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task UpdateASync(CommentEntity comment)
    {
        await context.SaveChangesAsync();
    }
}
