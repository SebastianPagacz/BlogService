using Blog.Application.Services;
using Blog.Domain;
using Blog.Domain.Repositories;
using Blog.Domain.Repositories.Repos;
using Blog.Domain.Seeders;
using BlogService.Middleware;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var dbConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? builder.Configuration.GetConnectionString("DefaultConnection");

        // Add services to the container.
        //builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("TestDb"));
        builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlServer(dbConnectionString), ServiceLifetime.Transient);
        builder.Services.AddScoped<IPostRepository, PostRepository>();
        builder.Services.AddScoped<ICommentRepository, CommentRepository>();

        // Redis config
        builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
        {
            var config = builder.Configuration.GetSection("ConnectionStrings")["Redis"];
            return ConnectionMultiplexer.Connect(config);
        });
        builder.Services.AddScoped<ICacheService, CacheService>();

        builder.Services.AddAutoMapper(typeof(AssemblyReference));
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Blog.Application.AssemblyReference).Assembly));

        builder.Services.AddScoped<IBlogSeeder, BlogSeeder>();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        // Exception handling middleware
        app.UseMiddleware<ExceptionHandlingMiddleware>();

        // Seeding test data
        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IBlogSeeder>();
        await seeder.SeedAsync();

        app.Run();
    }
}