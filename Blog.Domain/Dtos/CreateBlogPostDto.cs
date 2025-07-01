using System.ComponentModel.DataAnnotations;

namespace Blog.Domain.Dtos;

public class CreateBlogPostDto
{
    [MaxLength(60)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(2000)]
    public string Content { get; set; } = string.Empty;
}
