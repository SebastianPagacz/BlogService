using Blog.Domain.Models;
using Blog.Domain.Repositories;

namespace Blog.Domain.Seeders;

public class BlogSeeder(DataContext context) : IBlogSeeder
{
    public async Task SeedAsync()
    {
        if (!context.BlogPosts.Any())
        {
            var postsList = new List<BlogPostEntity>
            {
                new BlogPostEntity { Title = "Test1", Content = "Testowy content pierwszego posta. Testowa wiadomosc.", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new BlogPostEntity { Title = "Test2", Content = "Testowa tresc drugiego posta, tworze wiec jestem", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                new BlogPostEntity { Title = "Test3", Content = "Tak wyglada testowy post calkiem spoko nie ma ze nie", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
            };

            context.BlogPosts.AddRange(postsList);
            await context.SaveChangesAsync();
        }
        
        if (!context.Comments.Any())
        {
            var commentsList = new List<CommentEntity>
            {
                new CommentEntity { Title = "Mega fajny post", Content = "Serio kocham tego posta!", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PostId = 1 },
                new CommentEntity { Title = "No no no", Content = "Niezly jest ten post!", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PostId = 1 },
                new CommentEntity { Title = "hahah xd", Content = "Beka na calego xdd", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PostId = 1 },
                new CommentEntity { Title = "Spoko ten post", Content = "Spoko i tyle", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PostId = 2 },
                new CommentEntity { Title = "Wez zmien tytul pliska", Content = "Jak mozna dac taki tytul?", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PostId = 2 },
                new CommentEntity { Title = "Wiocha", Content = "Najslabszy post :(", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, PostId = 3 },
            };

            context.Comments.AddRange(commentsList);
            await context.SaveChangesAsync();
        }
    }
}
