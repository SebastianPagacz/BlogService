using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Domain.Models;

namespace User.Domain.Repositories;

public class UserDataContext : IdentityDbContext<UserEntity, RoleEntity, int>
{
    public DbSet<UserEntity> Users {  get; set; }
    public DbSet<RoleEntity> Roles {  get; set; }


    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }
}
