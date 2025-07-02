using AutoMapper;
using Blog.Domain.Dtos;
using Blog.Domain.Models;

namespace Blog.Domain.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<BlogPostEntity, BlogPostDto>();
    }
}
