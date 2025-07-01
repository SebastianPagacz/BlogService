using Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog.Domain.Repositories;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
    public DbSet<BlogPostEntity> BlogPosts { get; set; }
    public DbSet<CommentEntity> Comments { get; set; }
}
