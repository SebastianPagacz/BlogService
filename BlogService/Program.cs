using Blog.Domain;
using Blog.Domain.Profiles;
using Blog.Domain.Repositories;
using Blog.Domain.Repositories.Repos;
using Blog.Domain.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseInMemoryDatabase("TestDb"));
builder.Services.AddScoped<IPostRepository, PostRepository>();
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

// Seeding test data
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IBlogSeeder>();
await seeder.SeedAsync();

app.Run();
