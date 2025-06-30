using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Models;

public class CommentEntity
{
    [Required]
    public int Id { get; set; }
    
    [MaxLength(60)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(260)]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    // Post
    public int PostId { get; set; }
    public BlogPostEntity Post { get; set; } = new BlogPostEntity();
}
