using Blog.Domain.Models;

namespace Blog.Domain.Repositories.Repos;

public interface ICommentRepository
{
    Task<CommentEntity> AddAsync(CommentEntity comment);
    Task<List<CommentEntity>> GetAllAsync();
    Task<CommentEntity> GetByIdAsync(int id);
    Task UpdateASync(CommentEntity comment);
}
