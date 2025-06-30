using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Models;

public class BlogPostEntity
{
    [Required]
    public int Id { get; set; }
    
    [MaxLength(60)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsDeleted { get; set; } = false;

    // comments
    public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
}
