﻿namespace Blog.Domain.Dtos;

public class CreateCommentDto
{
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}
