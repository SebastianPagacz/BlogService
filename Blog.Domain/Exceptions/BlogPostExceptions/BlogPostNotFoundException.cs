namespace Blog.Domain.Exceptions.BlogPostExceptions;

public class BlogPostNotFoundException : Exception
{
    public BlogPostNotFoundException() : base() { }
    public BlogPostNotFoundException(string message) : base(message) { }
    public BlogPostNotFoundException(string message, Exception innerException) : base(message, innerException) { }

}
